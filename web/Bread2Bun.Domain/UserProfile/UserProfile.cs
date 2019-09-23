using Bread2Bun.Common.Extensions;
using Bread2Bun.Domain.Security;
using Bread2Bun.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Bread2Bun.Domain.UserProfile
{
    public class UserProfile : SocialMedia
    {
        public virtual long Id { get; protected set; }
        public virtual int CoverFoodImageId { get; protected set; }
        public virtual string AvailableDays { get; protected set; }
        public virtual string AboutMe { get; protected set; }
        public virtual string Languages { get; protected set; }
        public virtual List<UserFood> Foods { get; protected set; }
        public virtual string Country { get; set; }
        public virtual string City { get; set; }
        public virtual bool IsUpdated { get; set; }


        #region relationship
        [ForeignKey(nameof(Id))]
        public StoreUser User { get; protected set; }
        #endregion

        public UserProfile SetUserId(long userId)
        {
            Id = userId;
            return this;
        }

        public UserProfile Update(int coverfoodImageId, List<int> availableDays, string aboutMe, List<string> languages, string instagram, string twitter, string country, string city)
        {
            CoverFoodImageId = coverfoodImageId;
            AvailableDays = string.Join(",", availableDays);
            AboutMe = aboutMe;
            Languages = string.Join(",", languages);
            Instagram = instagram;
            Twitter = twitter;
            Country = country;
            City = city;
            IsUpdated = true;
            return this;
        }

        public UserProfile UpdateFoods(List<long> foodIds)
        {
            if (!foodIds.IsNullOrZero())
            {
                if (Foods.IsNullOrZero())
                {
                    Foods = new List<UserFood>();
                }
                Foods.RemoveAll(p => !foodIds.Contains(p.Id));
                var _foods = Foods;
                foodIds.ForEach(item =>
                {
                    try
                    {
                        var result = _foods.Where(p => p.Id == item).FirstOrDefault();
                        if (result.IsNull())
                        {
                            Foods.Add(new UserFood().Create(item, Id));
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                });
            }
            else
            {
                Foods.Clear();
            }
            return this;
        }
    }
}
