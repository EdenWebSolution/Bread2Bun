using Bread2Bun.Common;
using Bread2Bun.Domain.Chat;
using Bread2Bun.Domain.Food;
using Bread2Bun.Domain.Security;
using Bread2Bun.Domain.Shared;
using Bread2Bun.Domain.UserProfile;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bread2Bun.Data
{
    public class Bread2BunContext : IdentityDbContext<StoreUser, StoreUserRole, long>
    {
        private readonly UserResolverService userResolverService;

        public Bread2BunContext(DbContextOptions<Bread2BunContext> options, UserResolverService userResolverService) : base(options)
        {
            this.userResolverService = userResolverService;
        }

        #region Security
        public DbSet<Claims> Claim { get; set; }
        #endregion

        #region Shared
        public DbSet<Country> Country { get; set; }
        public DbSet<University> University { get; set; }
        #endregion

        #region Food
        public DbSet<Food> Foods { get; set; }
        #endregion

        #region Profile
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<UserFood> UserFood { get; set; }
        #endregion

        #region Chat
        public DbSet<Message> Message { get; set; }
        #endregion

        #region Overrides
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserFood>()
            .HasKey(o => new { o.Id, o.UserId, o.UserProfileId });

            ApplyFilters(builder);
            base.OnModelCreating(builder);
            builder.SeedDB();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var modifiedEntries = ChangeTracker.Entries<Audit>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (EntityEntry<Audit> entry in modifiedEntries)
            {
                long? userId = userResolverService.UserId;

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
            return base.SaveChangesAsync(cancellationToken);
        }
        #endregion
        public void ApplyFilters(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reviews>().HasQueryFilter(c => !c.IsDeleted);
        }
        #region filters

        #endregion
    }
}
