using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Service
{
    public abstract class ModelBase<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
        public long? CreatedById { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long? EditedId { get; set; }
        public DateTimeOffset? EditedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
