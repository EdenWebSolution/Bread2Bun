using Bread2Bun.Common.Enum;
using System;

namespace Bread2Bun.Common.Model
{
    public class StoreUserModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long? CreatedById { get; set; }
        public DateTimeOffset? EditedOn { get; set; }
        public long? EditedById { get; set; }
        public bool IsDeleted { get; set; }
        public Gender Gender { get; set; }
        public string ProfilePictureImagePath { get; set; }
    }
}
