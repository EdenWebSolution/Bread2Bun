using Bread2Bun.Common.Model;

namespace Bread2Bun.Service.Profile.Models.Review
{
    public class ReviewModel : ModelBase<long>
    {
        public long RevieweeId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public StoreUserModel Reviewer { get; set; }
        public string ReviewImage { get; set; }
    }
}
