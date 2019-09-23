
using Bread2Bun.Service.Food.Models;
using System.Collections.Generic;

namespace Bread2Bun.Service.Profile.Models.UserProfile
{
    public class UserProfileFoodViewResult
    {
        public UserProfileViewResult Profile { get; set; }
        public List<FoodViewResult> Foods { get; set; }
    }
}
