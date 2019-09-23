namespace Bread2Bun.Service.Food.Models
{
    public class FoodModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public string DefaultFoodImagepath { get; set; }
        public bool IsVegeterian { get; set; }
    }
}
