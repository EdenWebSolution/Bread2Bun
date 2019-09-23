namespace Bread2Bun.Service.Profile.Models.UserProfile
{
    public class Feed
    {
        public string Username { get; set; }
        public long UserId { get; set; }
        public string ProfileImage { get; set; }
        public string FoodImage { get; set; }
        public string FoodName { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string FoodDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsVegeterian { get; set; }
    }
}
