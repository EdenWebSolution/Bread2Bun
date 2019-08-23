using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service.Profile.Models.Review
{
    public class ReviewCreateModel
    {
        public long RevieweeId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
    }
}
