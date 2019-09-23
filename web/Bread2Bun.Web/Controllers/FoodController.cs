using Bread2Bun.Service.Food.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodController : BaseAPIController
    {
        private readonly IFoodService foodService;

        public FoodController(IFoodService foodService)
        {
            this.foodService = foodService;
        }

        [HttpGet("country/{countryId:int}")]
        public async Task<IActionResult> GetFoodsByCountry(int countryId)
        {
            try
            {
                var result = await foodService.GetFoodsByCountry(countryId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet("{foodid:int}/image")]
        public async Task<IActionResult> GetFoodsImage(int foodid)
        {
            try
            {
                var result = await foodService.GetFoodsImage(foodid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet("keyvalue")]
        public async Task<IActionResult> GetFoodKeyValue()
        {
            try
            {
                var result = await foodService.GetFoodKeyValue();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await foodService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

        [HttpGet("userfoods")]
        public async Task<IActionResult> GetFoodsByUser()
        {
            try
            {
                var result = await foodService.GetFoodsByUser(UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleExcpetion(ex);
            }
        }

    }
}