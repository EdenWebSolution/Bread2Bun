using AutoMapper;
using Bread2Bun.Common;
using Bread2Bun.Common.Constants;
using Bread2Bun.Common.Model;
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
            var entity = new Reviews().Create(reviewCreateModel.RevieweeId, userResolverService.UserId, reviewCreateModel.Review, reviewCreateModel.ReviewImage);
            context.Add(entity);
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

        public async Task<PageList<ReviewModel>> GetAll(PaginationBase paginationBase, long userId = 0)
        {
            var query = context.Reviews.Include(i => i.Reviewer).Where(w => w.RevieweeId == (userId != 0 ? userId : userResolverService.UserId));

            var totalNumberOfRecord = await query.CountAsync();

            query = query.OrderByDescending(x => x.CreatedOn).Skip(paginationBase.Skip).Take(paginationBase.Take);

            var entities = await query.AsNoTracking().ToListAsync();

            var resultData = mapper.Map<IEnumerable<ReviewModel>>(entities);

            foreach (var res in resultData)
            {
                res.ReviewImage = res.ReviewImage ?? FolderPath.ImagePath + FolderPath.Review + res.ReviewImage;
                res.Reviewer.ProfilePictureImagePath = res.Reviewer.ProfilePictureImagePath == null ? null : FolderPath.ImagePath + FolderPath.ProfileImages + res.Reviewer.ProfilePictureImagePath;
            }
            var resultSet = new PageList<ReviewModel>()
            {
                Skip = paginationBase.Skip,
                Take = paginationBase.Take,
                Items = resultData,
                TotalRecordCount = totalNumberOfRecord
            };
            return resultSet;
        }

        public async Task<ReviewModel> GetById(long id)
        {
            var entity = await context.Reviews.FirstOrDefaultAsync(f => f.Id == id);
            var model = mapper.Map<ReviewModel>(entity);
            model.ReviewImage = model.ReviewImage == null ? null : FolderPath.ImagePath + FolderPath.Review + model.ReviewImage;
            return model;
        }

        public async Task<ReviewModel> UpdateReviewAsync(ReviewUpdateModel reviewUpdateModel)
        {
            var entity = await context.Reviews.FirstOrDefaultAsync(f => f.Id == reviewUpdateModel.Id);
            entity.Update(reviewUpdateModel.Review, reviewUpdateModel.Rating);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return mapper.Map<ReviewModel>(entity);
        }
    }
}
