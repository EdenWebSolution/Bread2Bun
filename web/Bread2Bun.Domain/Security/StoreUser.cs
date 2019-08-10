using Bread2Bun.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bread2Bun.Domain.Security
{
    public class StoreUser : IdentityUser<long>
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        #region Relationships

        public virtual int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        public virtual int UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public University University { get; set; }

        #endregion
    }
}
