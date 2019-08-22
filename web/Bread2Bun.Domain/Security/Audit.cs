using Bread2Bun.Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bread2Bun.Domain.Security
{
    public abstract class Audit
    {
        public long? CreatedById { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long? EditedId { get; set; }
        public DateTimeOffset? EditedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
