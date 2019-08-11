using AutoMapper;
using Bread2Bun.Common.Model;
using Bread2Bun.Data;
using Bread2Bun.Domain.Security;
using Bread2Bun.Service.Security.Interface;
using Bread2Bun.Service.Security.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly Bread2BunContext bread2BunContext;
        private readonly IMapper mapper;
        private readonly UserManager<StoreUser> userManager;

        public SecurityService(Bread2BunContext bread2BunContext, IMapper mapper, UserManager<StoreUser> userManager)
        {
            this.bread2BunContext = bread2BunContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<StoreUserModel> CreateUser(CreateStoreUserModel createStoreUserModel)
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

    }
}
