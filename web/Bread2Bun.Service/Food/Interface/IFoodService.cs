using Bread2Bun.Service.Food.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Food.Interface
{
    public interface IFoodService
    {
        Task<List<KeyValuePair<long, string>>> GetFoodsByCountry(int countryId);
        Task<FoodModel> GetFoodsImage(int foodid);
        Task<List<KeyValuePair<long, string>>> GetFoodKeyValue();
        Task<FoodModel> GetById(int id);
        Task<List<FoodViewResult>> GetFoodsByUser(int userId);
    }
}
