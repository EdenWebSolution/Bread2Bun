using Bread2Bun.Domain.Security;
using Bread2Bun.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bread2Bun.Domain.Food
{
    public class Food : Audit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public string DefaultFoodImagepath { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
    }
}
