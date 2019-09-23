using Bread2Bun.Common.Model;
using System.Collections.Generic;

namespace Bread2Bun.Service.Profile.Models.UserProfile
{
    public class UserProfileViewResult : SocialMediaModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string University { get; set; }
        public List<int> AvailableDays { get; set; }
        public string AboutMe { get; set; }
        public List<string> Languages { get; set; }
        public AddressModel Address { get; set; }
    }
}
