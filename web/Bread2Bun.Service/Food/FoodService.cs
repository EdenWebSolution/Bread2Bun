using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bread2Bun.Common;
using Bread2Bun.Common.Constants;
using Bread2Bun.Data;
using Bread2Bun.Domain.Security;
using Bread2Bun.Service.Food.Interface;
using Bread2Bun.Service.Food.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bread2Bun.Service.Food
{
    public class FoodService : IFoodService
    {

        private readonly Bread2BunContext context;
        private readonly IMapper mapper;
        private readonly UserResolverService userResolverService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<StoreUser> userManager;

        public FoodService(Bread2BunContext bread2BunContext, IMapper mapper, UserResolverService userResolverService, UserManager<StoreUser> userManager)
        {
            this.context = bread2BunContext;
            this.mapper = mapper;
            this.userResolverService = userResolverService;
            this.userManager = userManager;
        }

        public async Task<List<KeyValuePair<long, string>>> GetFoodsByCountry(int countryId)
        {
            return await (from foods in context.Foods
                          where foods.CountryId == countryId
                          select new
                          {
                              foods.Id,
                              foods.Name
                          }).Select(p => new KeyValuePair<long, string>(p.Id, p.Name)).ToListAsync();
        }

        public async Task<FoodModel> GetFoodsImage(int foodid)
        {
            var res = await context.Foods.FirstOrDefaultAsync(p => p.Id == foodid);
            res.DefaultFoodImagepath = FolderPath.ImagePath + FolderPath.FoodImages + res.DefaultFoodImagepath;
            return mapper.Map<FoodModel>(res);
        }

        public async Task<List<KeyValuePair<long, string>>> GetFoodKeyValue()
        {
            return await (from foods in context.Foods
                          select new
                          {
                              foods.Id,
                              foods.Name
                          }).Select(p => new KeyValuePair<long, string>(p.Id, p.Name)).ToListAsync();
        }

        public async Task<FoodModel> GetById(int id)
        {
            var res = await context.Foods.FirstOrDefaultAsync(p => p.Id == id);
            return mapper.Map<FoodModel>(res);
        }

        public async Task<List<FoodViewResult>> GetFoodsByUser(int userId)
        {
            var res = await context.Users.FirstOrDefaultAsync(p => p.Id == userId);
            if (res == null)
            {
                throw new Exception("User not found");
            }

            return await (from userfood in context.UserFood
                          join foods in context.Foods on userfood.Id equals foods.Id
                          join country in context.Country on foods.CountryId equals country.Id
                          where userfood.UserProfileId == userId
                          select new FoodViewResult
                          {
                              Id = foods.Id,
                              FullName = res.FullName,
                              Name = foods.Name,
                              DefaultFoodImagepath = foods.DefaultFoodImagepath == null ? null : FolderPath.ImagePath + FolderPath.FoodImages + foods.DefaultFoodImagepath,
                              Country = country.Name,
                              Description = foods.Description,
                              CountryCode = country.CountryCode,
                              IsVegeterian = foods.IsVegetarian,
                          }).ToListAsync();
        }

    }
}
