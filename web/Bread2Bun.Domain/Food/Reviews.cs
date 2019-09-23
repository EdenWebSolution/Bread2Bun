using Bread2Bun.Domain.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bread2Bun.Domain.Food
{
    public class Reviews : Audit
    {
        public long Id { get; protected set; }
        public long RevieweeId { get; protected set; }
        [ForeignKey(nameof(RevieweeId))]
        public StoreUser Reviewee { get; protected set; }
        public long ReviewerId { get; protected set; }
        [ForeignKey(nameof(ReviewerId))]
        public StoreUser Reviewer { get; protected set; }
        public string Review { get; protected set; }
        public int Rating { get; protected set; }
        public string ReviewImage { get; set; }


        public Reviews Create(long revieweeId,long reviewerId,string review,string reviewImage)
        {
            RevieweeId = revieweeId;
            ReviewerId = reviewerId;
            Review = review;
            ReviewImage = reviewImage;
            return this;
        }

        public Reviews Update(string review, int rating)
        {
            Review = review;
            Rating = rating;
            ReviewImage = review;
            return this;
        }

        public Reviews Delete()
        {
            IsDeleted = true;
            return this;
        }
    }
}
