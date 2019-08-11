using Bread2Bun.Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bread2Bun.Domain.Security
{
    public abstract class Audit
    {
        [NotMapped]
        public StoreUserModel User { get; set; }
        public long? CreatedById { get; protected set; }
        public DateTimeOffset CreatedOn { get; protected set; } = DateTimeOffset.UtcNow;
        public long? EditedId { get; protected set; }
        public DateTimeOffset? EditedOn { get; protected set; }
        public bool IsDeleted { get; protected set; }

        protected void AuditCreate()
        {

        }

        protected void AuditEdit()
        {
            EditedOn = DateTimeOffset.UtcNow;
        }
    }
}
