using Bread2Bun.Domain.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bread2Bun.Domain.Food
{
    public class Reviews : Audit
    {
        public long Id { get; protected set; }
        public long RevieweeId { get; protected set; }
        [ForeignKey(nameof(RevieweeId))]
        public StoreUser Reviewee { get; protected set; }
        public string Review { get; protected set; }
        public int Rating { get; protected set; }

        public Reviews Update(string review)
        {
            Review = review;
            return this;
        }

        public Reviews Delete()
        {
            IsDeleted = true;
            return this;
        }
    }
}
