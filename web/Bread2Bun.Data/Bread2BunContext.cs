using Bread2Bun.Domain.Security;
using Bread2Bun.Domain.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bread2Bun.Data
{
    public class Bread2BunContext : IdentityDbContext<StoreUser, StoreUserRole, long>
    {
        public Bread2BunContext(DbContextOptions<Bread2BunContext> options) : base(options) { }

        #region Shared
        public DbSet<Country> Country { get; set; }
        public DbSet<University> University { get; set; }
        #endregion
    }
}
