using AutoMapper;
using Bread2Bun.Common.Model;
using Bread2Bun.Data;
using Bread2Bun.Domain.Security;
using Bread2Bun.Service.Security.Interface;
using Bread2Bun.Service.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Bread2Bun.Common.Constants;

namespace Bread2Bun.Service.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly Bread2BunContext bread2BunContext;
        private readonly IMapper mapper;
        private readonly UserManager<StoreUser> userManager;
        private readonly SignInManager<StoreUser> signInManager;
        private readonly IConfiguration configuration;

        public SecurityService(Bread2BunContext bread2BunContext,
            IMapper mapper, UserManager<StoreUser> userManager,
            SignInManager<StoreUser> signInManager, IConfiguration configuration)
        {
            this.bread2BunContext = bread2BunContext;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<StoreUserModel> CreateUserAsync(CreateStoreUserModel createStoreUserModel)
        {
            var storeUser = mapper.Map<StoreUser>(createStoreUserModel);

            var user = await userManager.CreateAsync(storeUser, createStoreUserModel.Password);

            if (user.Succeeded)
            {
                var result = mapper.Map<StoreUserModel>(storeUser);
                return result;
            }

            else
            {
                var error = user.Errors.FirstOrDefault();
                throw new ArgumentException(error.Description);
            }
        }

        public async Task<AuthTokenModel> LoginAsync(LoginModel loginModel)
        {
            var user = await userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && !user.IsDeleted)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, false);

                if (result.Succeeded)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                        new Claim(CustomClaims.IsAdmin,user.IsAdmin.ToString()),
                        new Claim(CustomClaims.RememberMe,loginModel.RememberMe.ToString()),
                    };

                    var token = new JwtSecurityToken(
                        configuration["Tokens:Issuer"],
                        configuration["Tokens:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddYears(1),
                        signingCredentials: credentials
                        );

                    var generatedToken = new AuthTokenModel(token);

                    return generatedToken;
                }
            }
            throw new UnauthorizedAccessException("Invalid usernamr or password");
        }
    }
}
