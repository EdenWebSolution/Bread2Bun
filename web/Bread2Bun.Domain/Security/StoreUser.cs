﻿using Bread2Bun.Common.Enum;
using Bread2Bun.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bread2Bun.Domain.Security
{
    public class StoreUser : IdentityUser<long>
    {
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual DateTimeOffset CreatedOn { get; protected set; } = DateTimeOffset.UtcNow;
        public virtual long? CreatedById { get; protected set; }
        public virtual DateTimeOffset? EditedOn { get; protected set; }
        public virtual long? EditedById { get; protected set; }
        public virtual bool IsDeleted { get; protected set; }
        public virtual Gender Gender { get; protected set; }
        public virtual bool IsAdmin { get; protected set; }
        public virtual string ProfilePictureImagePath { get; protected set; }


        #region Relationships
        public virtual int CountryId { get; protected set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; protected set; }

        public virtual int UniversityId { get; protected set; }
        [ForeignKey(nameof(UniversityId))]
        public University University { get; protected set; }

        public StoreUser Update(string firstName,string lastName,string email,int countryId,int universityId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CountryId = countryId;
            UniversityId = universityId;
            return this;
        }

        public StoreUser UpdateImage(string profilePictureImage)
        {
            ProfilePictureImagePath = profilePictureImage;
            return this;
        }
        #endregion
    }
}
