using AutoMapper;
using Bread2Bun.Common;
using Bread2Bun.Data;
using Bread2Bun.Domain.Food;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Profile.Models.Review;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Profile
{
    public class ReviewService : IReviewService
    {
        private readonly Bread2BunContext context;
        private readonly IMapper mapper;
        private readonly UserResolverService userResolverService;

        public ReviewService(Bread2BunContext bread2BunContext, IMapper mapper, UserResolverService userResolverService)
        {
            this.context = bread2BunContext;
            this.mapper = mapper;
            this.userResolverService = userResolverService;
        }
        public async Task<ReviewModel> AddReviewAsync(ReviewCreateModel reviewCreateModel)
        {
            var entity = mapper.Map<Reviews>(reviewCreateModel);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return mapper.Map<ReviewModel>(entity);
        }

        public async Task<ReviewModel> DeleteReviewAsync(long id)
        {
            var entity = await context.Reviews.FirstOrDefaultAsync(f => f.Id == id);
            entity.Delete();
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return mapper.Map<ReviewModel>(entity);
        }

        public async Task<PaginationModel<ReviewModel>> GetAll(PaginationBase paginationBase)
        {
            var query = context.Reviews.AsQueryable();

            var totalNumberOfRecord = await query.CountAsync();

            query = query.OrderByDescending(x => x.CreatedOn).Skip(paginationBase.Skip).Take(paginationBase.Take);

            var entities = await query.AsNoTracking().ToListAsync();

            var resultData = mapper.Map<IEnumerable<ReviewModel>>(entities);

            var resultSet = new PaginationModel<ReviewModel>()
            {
                TotalRecords = totalNumberOfRecord,
                Details = resultData
            };
            return resultSet;
        }

        public async Task<ReviewModel> GetById(long id)
        {
            var entity = await context.Reviews.FirstOrDefaultAsync(f =>  f.Id == id);
            var model = mapper.Map<ReviewModel>(entity);
            return model;
        }

        public async Task<ReviewModel> UpdateReviewAsync(ReviewUpdateModel reviewUpdateModel)
        {
            var entity = await context.Reviews.FirstOrDefaultAsync(f => f.Id == reviewUpdateModel.Id);
            entity.Update(reviewUpdateModel.Review);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return mapper.Map<ReviewModel>(entity);
        }
    }
}
