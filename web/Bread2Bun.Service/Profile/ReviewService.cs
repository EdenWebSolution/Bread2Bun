using AutoMapper;
using Bread2Bun.Common;
using Bread2Bun.Data;
using Bread2Bun.Domain.Food;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Profile.Models.Review;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Profile
{
    public class ReviewService : IReviewService
    {
        private readonly Bread2BunContext bread2BunContext;
        private readonly IMapper mapper;
        private readonly UserResolverService userResolverService;

        public ReviewService(Bread2BunContext bread2BunContext, IMapper mapper, UserResolverService userResolverService)
        {
            this.bread2BunContext = bread2BunContext;
            this.mapper = mapper;
            this.userResolverService = userResolverService;
        }
        public async Task<ReviewModel> AddReviewAsync(ReviewCreateModel reviewCreateModel)
        {
            var entity = mapper.Map<Reviews>(reviewCreateModel);
            await bread2BunContext.AddAsync(entity);
            await bread2BunContext.SaveChangesAsync();
            return mapper.Map<ReviewModel>(entity);
        }

        public Task<ReviewModel> GetReviewByIdAsync(long reviewId)
        {
            throw new NotImplementedException();
        }

        public async Task<ReviewModel> UpdateReviewAsync(ReviewUpdateModel reviewUpdateModel)
        {
            var entity = await bread2BunContext.Reviews.FirstOrDefaultAsync(f => f.Id == reviewUpdateModel.Id);
            entity.Update(reviewUpdateModel.Review);
            bread2BunContext.Entry(entity).State = EntityState.Modified;
            await bread2BunContext.SaveChangesAsync();
            return mapper.Map<ReviewModel>(entity);
        }
    }
}
