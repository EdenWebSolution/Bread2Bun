using Bread2Bun.Domain.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bread2Bun.Domain.UserProfile
{
    public class UserProfile : Audit
    {
        public long Id { get; protected set; }
        [ForeignKey(nameof(Id))]
        public StoreUser User { get; protected set; }
        public string AboutMe { get; protected set; }
        public string Facebook { get; protected set; }
        public string Instagram { get; protected set; }
        public string Twitter { get; protected set; }

        public UserProfile SetUserId(long userId)
        {
            Id = userId;
            return this;
        }
    }
}
