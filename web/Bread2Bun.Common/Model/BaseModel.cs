using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Common.Model
{
    public abstract class BaseModel
    {
        public long? CreatedById { get; protected set; }
        public long? EditedId { get; protected set; }
        public bool IsDeleted { get; set; }
    }
}
