using Bread2Bun.Domain.Security;
using Bread2Bun.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bread2Bun.Domain.Food
{
    public class Food : Audit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVegetarian { get; set; }
        public int CountryId { get; set; }
        public string DefaultFoodImagepath { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        public Food Create(string name,string description,int countryId,string defaultImagePath)
        {
            Name = name;
            Description = description;
            CountryId = countryId;
            DefaultFoodImagepath = defaultImagePath;
            return this;
        }
    }
}
