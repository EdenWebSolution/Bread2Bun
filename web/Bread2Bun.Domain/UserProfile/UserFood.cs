using Bread2Bun.Domain.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bread2Bun.Domain.UserProfile
{
    public class UserFood
    {
        public long Id { get; protected set; }
        public long UserId { get; set; }
        public long UserProfileId { get; set; }

        #region Relationships
        [ForeignKey(nameof(Id))]
        public Domain.Food.Food Food { get; protected set; }
        [ForeignKey(nameof(UserId))]
        public StoreUser StoreUser { get; protected set; }
        [ForeignKey(nameof(UserProfileId))]
        public UserProfile UserProfile { get; protected set; }
        #endregion

        public UserFood Create(long id, long userId)
        {
            Id = id;
            UserId = userId;
            return this;
        }
    }
}
