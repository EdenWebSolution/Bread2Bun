using AutoMapper;
using Bread2Bun.Common;
using Bread2Bun.Common.Constants;
using Bread2Bun.Common.Model;
using Bread2Bun.Data;
using Bread2Bun.Domain.Security;
using Bread2Bun.Domain.UserProfile;
using Bread2Bun.Service.Food.Models;
using Bread2Bun.Service.Profile.Interface;
using Bread2Bun.Service.Profile.Models;
using Bread2Bun.Service.Profile.Models.UserProfile;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly Bread2BunContext context;
        private readonly IMapper mapper;
        private readonly UserResolverService userResolverService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<StoreUser> userManager;

        public ProfileService(Bread2BunContext bread2BunContext, IMapper mapper, UserResolverService userResolverService, UserManager<StoreUser> userManager)
        {
            this.context = bread2BunContext;
            this.mapper = mapper;
            this.userResolverService = userResolverService;
            this.userManager = userManager;
        }

        public async Task<BasicInfoModel> GetBasicInfo(long userId)
        {
            var user = await userManager.Users.Include(i => i.Country).Include(i => i.University).FirstOrDefaultAsync(f => f.Id == (userId != 0 ? userId : userResolverService.UserId));
            return mapper.Map<BasicInfoModel>(user);
        }

        public async Task CreateUserProfile(long userId)
        {
            var entity = new UserProfile().SetUserId(userId);
            await context.UserProfile.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<UserProfile> UpdateUserProfile(UserProfileUpdateModel model)
        {
            //using (var transaction = context.Database.BeginTransaction())
            //{
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(f => f.Id == userResolverService.UserId);
                user.Update(model.FirstName, model.LastName, model.Email, model.CountryId, model.UniversityId);
                // await context.SaveChangesAsync();

                var entity = await context.UserProfile.Include(p => p.Foods).FirstOrDefaultAsync(f => f.Id == userResolverService.UserId);
                entity.Update(model.CoverFoodImageId, model.AvailableDays, model.AboutMe, model.Languages, model.Instagram, model.Twitter, model.Address.Country, model.Address.City).UpdateFoods(model.FoodIds);
                await context.SaveChangesAsync();

                //transaction.Commit();
                return mapper.Map<UserProfile>(entity);
            }
            catch (System.Exception e)
            {
                //transaction.Rollback();
                throw new System.Exception(e.Message);
            }
            //}
        }

        public async Task<UserProfileUpdateModel> GetUserProfileInfo(int userId)
        {
            var userProfile = await context.UserProfile
                                        .Include(p => p.Foods)
                                        .Include(p => p.User)
                                        .FirstOrDefaultAsync(p => p.Id == userId);

            return new UserProfileUpdateModel()
            {
                FirstName = userProfile.User.FirstName,
                LastName = userProfile.User.LastName,
                Email = userProfile.User.Email,
                ProfileImage = userProfile.User.ProfilePictureImagePath == null ? null : FolderPath.ImagePath + FolderPath.ProfileImages + userProfile.User.ProfilePictureImagePath,
                UniversityId = userProfile.User.UniversityId,
                CountryId = userProfile.User.CountryId,
                CoverFoodImageId = userProfile.CoverFoodImageId,
                AvailableDays = userProfile.AvailableDays == null ? new List<int>() : userProfile.AvailableDays.Split(',').Select(int.Parse).ToList(),
                AboutMe = userProfile.AboutMe,
                Languages = userProfile.AvailableDays == null ? new List<string>() : userProfile.Languages.Split(',').ToList<string>(),
                FoodIds = userProfile.Foods == null ? new List<long>() : userProfile.Foods.Select(p => p.Id).ToList<long>(),
                Instagram = userProfile.Instagram,
                Twitter = userProfile.Twitter,
                Address = new Common.Model.AddressModel { City = userProfile.City, Country = userProfile.Country }
            };
        }

        public async Task UpdateUserProfileImage(int userId, string fileName)
        {
            var user = await context.Users.FirstOrDefaultAsync(p => p.Id == userId);
            user.UpdateImage(fileName);
            await context.SaveChangesAsync();
        }

        public async Task<UserProfileViewResult> GetUserProfileViewInfo(int userId)
        {
            var userProfile = await context.UserProfile
                                        .Include(p => p.User)
                                        .ThenInclude(p => p.University)
                                        .Include(p => p.User)
                                        .ThenInclude(p => p.Country)
                                        .FirstOrDefaultAsync(p => p.Id == userId);
            var coverImage = new Domain.Food.Food();
            if (userProfile.CoverFoodImageId != 0)
            {
                coverImage = await context.Foods.FirstOrDefaultAsync(p => p.Id == userProfile.CoverFoodImageId);
            }

            return new UserProfileViewResult()
            {
                UserId = userProfile.User.Id,
                UserName = userProfile.User.UserName,
                FirstName = userProfile.User.FirstName,
                LastName = userProfile.User.LastName,
                Email = userProfile.User.Email,
                ProfileImage = userProfile.User.ProfilePictureImagePath == null ? null : FolderPath.ImagePath + FolderPath.ProfileImages + userProfile.User.ProfilePictureImagePath,
                CoverImage = coverImage.DefaultFoodImagepath == null ? null : FolderPath.ImagePath + FolderPath.FoodImages + coverImage.DefaultFoodImagepath,
                University = userProfile.User.University.Name,
                Country = userProfile.User.Country.Name,
                CountryCode = userProfile.User.Country.CountryCode,
                AvailableDays = userProfile.AvailableDays == null ? new List<int>() : userProfile.AvailableDays.Split(',').Select(int.Parse).ToList(),
                AboutMe = userProfile.AboutMe,
                Languages = userProfile.AvailableDays == null ? new List<string>() : userProfile.Languages.Split(',').ToList<string>(),
                Instagram = userProfile.Instagram,
                Twitter = userProfile.Twitter,
                Address = new Common.Model.AddressModel { City = userProfile.City, Country = userProfile.Country }
            };
        }

        public async Task<PageList<Feed>> GetFeeds(int userId, int skip, int take)
        {
            var feedList = (from user in context.Users
                            join userprofie in context.UserProfile on user.Id equals userprofie.Id
                            join userfood in context.UserFood on userprofie.Id equals userfood.UserProfileId
                            join foods in context.Foods on userfood.Id equals foods.Id
                            join country in context.Country on foods.CountryId equals country.Id
                            where userfood.UserProfileId != userId
                            select new Feed
                            {
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                UserId = user.Id,
                                Username = user.UserName,
                                ProfileImage = user.ProfilePictureImagePath == null ? null : FolderPath.ImagePath + FolderPath.ProfileImages + user.ProfilePictureImagePath,
                                FoodName = foods.Name,
                                FoodImage = foods.DefaultFoodImagepath == null ? null : FolderPath.ImagePath + FolderPath.FoodImages + foods.DefaultFoodImagepath,
                                Country = country.Name,
                                FoodDescription = foods.Description,
                                CountryCode = country.CountryCode,
                                IsVegeterian = foods.IsVegetarian
                            }).AsQueryable();

            var itemcount = feedList.Count();
            feedList = (take != 0) ? feedList.OrderBy(a => Guid.NewGuid()).Skip(skip).Take(take) : feedList;
            var shuffledList = await feedList.ToListAsync();

            return new PageList<Feed>
            {
                Skip = skip,
                Take = take,
                Items = shuffledList,
                TotalRecordCount = itemcount
            };
        }

        public async Task<UserProfileFoodViewResult> GetUserProfileInfoByUserName(string username)
        {
            var res = await context.Users.FirstOrDefaultAsync(p => p.UserName == username);
            if (res == null)
            {
                throw new Exception("User not found");
            }
            var userProfileFoodViewResult = new UserProfileFoodViewResult();
            var userProfile = await context.UserProfile
                                        .Include(p => p.User)
                                        .ThenInclude(p => p.University)
                                        .Include(p => p.User)
                                        .ThenInclude(p => p.Country)
                                        .FirstOrDefaultAsync(p => p.Id == res.Id);
            var coverImage = new Domain.Food.Food();
            if (userProfile.CoverFoodImageId != 0)
            {
                coverImage = await context.Foods.FirstOrDefaultAsync(p => p.Id == userProfile.CoverFoodImageId);
            }

            userProfileFoodViewResult.Profile = new UserProfileViewResult()
            {
                UserId = userProfile.User.Id,
                UserName = userProfile.User.UserName,
                FirstName = userProfile.User.FirstName,
                LastName = userProfile.User.LastName,
                Email = userProfile.User.Email,
                ProfileImage = userProfile.User.ProfilePictureImagePath == null ? null : FolderPath.ImagePath + FolderPath.ProfileImages + userProfile.User.ProfilePictureImagePath,
                CoverImage = coverImage.DefaultFoodImagepath == null ? null : FolderPath.ImagePath + FolderPath.FoodImages + coverImage.DefaultFoodImagepath,
                University = userProfile.User.University.Name,
                Country = userProfile.User.Country.Name,
                CountryCode = userProfile.User.Country.CountryCode,
                AvailableDays = userProfile.AvailableDays == null ? new List<int>() : userProfile.AvailableDays.Split(',').Select(int.Parse).ToList(),
                AboutMe = userProfile.AboutMe,
                Languages = userProfile.AvailableDays == null ? new List<string>() : userProfile.Languages.Split(',').ToList<string>(),
                Instagram = userProfile.Instagram,
                Twitter = userProfile.Twitter,
                Address = new Common.Model.AddressModel { City = userProfile.City, Country = userProfile.Country }
            };

            userProfileFoodViewResult.Foods = await (from userfood in context.UserFood
                                                     join foods in context.Foods on userfood.Id equals foods.Id
                                                     join country in context.Country on foods.CountryId equals country.Id
                                                     where userfood.UserProfileId == res.Id
                                                     select new FoodViewResult
                                                     {
                                                         Id = foods.Id,
                                                         FirstName = res.FirstName,
                                                         LastName = res.LastName,
                                                         Name = foods.Name,
                                                         DefaultFoodImagepath = foods.DefaultFoodImagepath == null ? null : FolderPath.ImagePath + FolderPath.FoodImages + foods.DefaultFoodImagepath,
                                                         Country = country.Name,
                                                         Description = foods.Description,
                                                         CountryCode = country.CountryCode,
                                                         IsVegeterian = foods.IsVegetarian,
                                                     }).ToListAsync();

            return userProfileFoodViewResult;
        }

        public async Task<bool> GetUserProfileStatus(int userId)
        {
            var userProfile = await context.UserProfile.FirstOrDefaultAsync(p => p.Id == userId);
            return userProfile.IsUpdated;
        }
    }
}
