using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Profile.Models.Review
{
    public class ReviewUpdateModel
    {
        public long Id { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
    }
}
