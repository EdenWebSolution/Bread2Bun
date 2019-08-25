using AutoMapper;
using Bread2Bun.Common;
using Bread2Bun.Data;
using Bread2Bun.Domain.Security;
using Bread2Bun.Domain.UserProfile;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Profile.Models;
using Bread2Bun.Service.Profile.Models.UserProfile;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly Bread2BunContext context;
        private readonly IMapper mapper;
        private readonly UserResolverService userResolverService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<StoreUser> userManager;

        public ProfileService(Bread2BunContext bread2BunContext, IMapper mapper, UserResolverService userResolverService, UserManager<StoreUser> userManager)
        {
            this.context = bread2BunContext;
            this.mapper = mapper;
            this.userResolverService = userResolverService;
            this.userManager = userManager;
        }

        public async Task<BasicInfoModel> GetBasicInfo(long userId)
        {
            var user = await userManager.Users.Include(i => i.Country).Include(i => i.University).FirstOrDefaultAsync(f => f.Id == (userId != 0 ? userId : userResolverService.UserId));
            return mapper.Map<BasicInfoModel>(user);
        }

        public async Task CreateUserProfile(long userId)
        {
            var entity = new UserProfile().SetUserId(userId);

            await context.UserProfile.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<UserProfileModel> UpdateUserProfile(UserProfileUpdateModel model)
        {
            var entity = await context.UserProfile.FirstOrDefaultAsync(f => f.Id == userResolverService.UserId);
            entity.Update(model.AboutMe, model.Facebook, model.Instagram, model.Twitter);
            await context.SaveChangesAsync();
            return mapper.Map<UserProfileModel>(entity);
        }
    }
}
