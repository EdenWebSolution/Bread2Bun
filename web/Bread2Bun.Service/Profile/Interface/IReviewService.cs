using Bread2Bun.Service.Profile.Models.Review;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Profile.Interface
{
    public interface IReviewService
    {
        Task<ReviewModel> AddReviewAsync(ReviewCreateModel reviewCreateModel);
        Task<ReviewModel> GetReviewByIdAsync(long reviewId);
        Task<ReviewModel> UpdateReviewAsync(ReviewUpdateModel reviewUpdateModel);
        Task<ReviewModel> DeleteReviewAsync(long id);
    }
}
