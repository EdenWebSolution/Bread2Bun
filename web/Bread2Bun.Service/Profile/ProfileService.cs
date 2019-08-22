using AutoMapper;
using Bread2Bun.Common;
using Bread2Bun.Data;
using Bread2Bun.Domain.Security;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Profile.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly Bread2BunContext bread2BunContext;
        private readonly IMapper mapper;
        private readonly UserResolverService userResolverService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<StoreUser> userManager;

        public ProfileService(Bread2BunContext bread2BunContext, IMapper mapper, UserResolverService userResolverService, UserManager<StoreUser> userManager)
        {
            this.bread2BunContext = bread2BunContext;
            this.mapper = mapper;
            this.userResolverService = userResolverService;
            this.userManager = userManager;
        }

        public async Task<BasicInfoModel> GetBasicInfo()
        {
            var user = await userManager.Users.Include(i => i.Country).Include(i => i.University).FirstOrDefaultAsync(f => f.Id == userResolverService.UserId);
            var ma = mapper.Map<BasicInfoModel>(user);
            return ma;
        }
    }
}
