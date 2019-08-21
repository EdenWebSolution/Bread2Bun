using Bread2Bun.Common;
using Bread2Bun.Domain.Security;
using Bread2Bun.Domain.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace Bread2Bun.Data
{
    public class Bread2BunContext : IdentityDbContext<StoreUser, StoreUserRole, long>
    {
        private readonly UserResolverService userService;

        public Bread2BunContext(DbContextOptions<Bread2BunContext> options) : base(options)
        {
        }

        //public Bread2BunContext(UserResolverService userService)
        //{
        //    this.userService = userService;
        //}

        #region security
        public DbSet<Claims> Claim { get; set; }
        #endregion

        #region Shared
        public DbSet<Country> Country { get; set; }
        public DbSet<University> University { get; set; }
        #endregion


        #region Overides
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.SeedDB();
        }

        public override int SaveChanges()
        {
            long? userId = null;
           
            var modifiedEntries = ChangeTracker.Entries<Audit>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (EntityEntry<Audit> entry in modifiedEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedById = userId;
                    entry.Entity.CreatedOn = DateTimeOffset.Now;
                }

                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.EditedId = userId;
                    entry.Entity.EditedOn = DateTimeOffset.Now;
                }
            }

            return base.SaveChanges();
        }


        #endregion
    }
}
