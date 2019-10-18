namespace Bread2Bun.Service.Food.Models
{
    public class FoodViewResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public bool IsVegeterian { get; set; }
        public string DefaultFoodImagepath { get; set; }
        public string FullName { get; set; }
    }
}
