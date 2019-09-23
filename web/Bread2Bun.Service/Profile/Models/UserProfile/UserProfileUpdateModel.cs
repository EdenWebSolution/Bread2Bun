using Bread2Bun.Common.Model;
using System.Collections.Generic;

namespace Bread2Bun.Service.Profile.Models.UserProfile
{
    public class UserProfileUpdateModel : SocialMediaModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public int CountryId { get; set; }
        public int UniversityId { get; set; }
        public int CoverFoodImageId { get; set; }
        public List<int> AvailableDays { get; set; }
        public string AboutMe { get; set; }
        public List<string> Languages { get; set; }
        public List<long> FoodIds { get; set; }
        public AddressModel Address { get; set; }
    }
}
