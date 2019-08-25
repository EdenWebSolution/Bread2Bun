using Bread2Bun.Service.Profile.Models;
using Bread2Bun.Service.Profile.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Profile.Interface
{
    public interface IProfileService
    {
        Task<BasicInfoModel> GetBasicInfo(long userId = 0);
        Task<UserProfileModel> CreateUserProfile(UserProfileCreateModel userProfileCreateModel);
        Task<UserProfileModel> UpdateUserProfile(UserProfileUpdateModel userProfileUpdateModel);
    }
}
