using AutoMapper;
using Bread2Bun.Common;
using Bread2Bun.Common.Constants;
using Bread2Bun.Common.Mailer;
using Bread2Bun.Common.Model;
using Bread2Bun.Data;
using Bread2Bun.Domain.Security;
using Bread2Bun.Service.Security.Interface;
using Bread2Bun.Service.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
                var token = await userManager.GenerateEmailConfirmationTokenAsync(storeUser);
                var confirmPasswordLink = string.Concat(GlobalConfig.LocalBaseUrl, $"/confirmemail?token={token}&email={storeUser.Email}");
                var result = mapper.Map<StoreUserModel>(storeUser);


                var messageBuilder = new EmailBuilder(configuration)
                {
                    To = new[] { storeUser.Email },
                    Subject = "Welcome To Bread2Bun",
                    IsBodyHtml = true,
                    Body = $"Hi {storeUser.FirstName} {storeUser.LastName}, please click on the link below so that we can confirm your email address. " +
                    $"\n\n\n\nHappy eating!"

                };

                return result;
            }

            else
            {
                var error = user.Errors.FirstOrDefault();
                throw new ArgumentException(error.Description);
            }
        }

        public async Task ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel)
        {
            var user = await userManager.FindByEmailAsync(confirmEmailModel.Email);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, confirmEmailModel.Token);
                if (!result.Succeeded)
                {
                    var error = result.Errors.FirstOrDefault();
                    throw new ArgumentException(error.Description);
                }
            }
        }

        public async Task<AuthTokenModel> LoginAsync(LoginModel loginModel)
        {
            var user = await userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && !user.IsDeleted)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, false);
                if (result.IsNotAllowed)
                {
                    throw new UnauthorizedAccessException("Your email has not been confirmed, please confirm your email address");
                }
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
            throw new AuthenticationException("Invalid usernamr or password");
        }

        public async Task ForgotPasswordAsync(ForgotPassowrdModel forgotPassowrdModel)
        {
            var user = await userManager.FindByEmailAsync(forgotPassowrdModel.Email);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var passwordReseLink = string.Concat(GlobalConfig.LocalBaseUrl, $"/resetpassword?token={token}&email={user.Email}");

                //var messageBuilder = new EmailBuilder()
                //{
                //    To = user.Email,
                //    Subject = "Reset Password",
                //    IsBodyHtml = true,
                //    Body = BuildMailTemplate.CreateContactUsTemplate(sendAMessageViewModel)
                //};
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user != null)
            {
                var result = await userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
                if (!result.Succeeded)
                {
                    var error = result.Errors.FirstOrDefault();
                    throw new ArgumentException(error.Description);
                }
            }
        }
    }
}
