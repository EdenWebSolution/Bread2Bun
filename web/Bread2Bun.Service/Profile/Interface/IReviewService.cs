using Bread2Bun.Common;
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
        Task<ReviewModel> UpdateReviewAsync(ReviewUpdateModel reviewUpdateModel);
        Task<ReviewModel> DeleteReviewAsync(long id);
        Task<PaginationModel<ReviewModel>> GetAll(PaginationBase paginationBase, long userId = 0);
        Task<ReviewModel> GetById(long id);
    }
}
