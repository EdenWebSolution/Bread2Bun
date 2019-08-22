using Bread2Bun.Domain.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bread2Bun.Domain.Food
{
    public class Reviews : Audit
    {
        public long Id { get; set; }
        public long RevieweeId { get; set; }
        [ForeignKey(nameof(RevieweeId))]
        public StoreUser Reviewee { get; set; }
        public string Review { get; set; }
    }
}
