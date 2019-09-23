using Bread2Bun.Common.Model;
using Bread2Bun.Domain.UserProfile;
using Bread2Bun.Service.Profile.Models;
using Bread2Bun.Service.Profile.Models.UserProfile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Profile.Interface
{
    public interface IProfileService
    {
        Task<BasicInfoModel> GetBasicInfo(long userId = 0);
        Task CreateUserProfile(long userId);
        Task<UserProfile> UpdateUserProfile(UserProfileUpdateModel userProfileUpdateModel);
        Task<UserProfileUpdateModel>  GetUserProfileInfo(int userId);
        Task UpdateUserProfileImage(int userId, string fileName);
        Task<UserProfileViewResult> GetUserProfileViewInfo(int userId);
        Task<PageList<Feed>> GetFeeds(int userId, int skip, int take);
        Task<UserProfileFoodViewResult> GetUserProfileInfoByUserName(string username);
        Task<bool> GetUserProfileStatus(int userId);
    }
}
