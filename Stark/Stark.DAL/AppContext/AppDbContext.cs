using Microsoft.EntityFrameworkCore;
using Stark.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Stark.DAL
{
    public class AppDbContext : DbContext
    {
        public Func<DateTime> TimestampProvider { get; set; } = () => DateTime.UtcNow;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<WebLink> WebLinks { get; set; }


        private void TrackChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (!(entry.Entity is IAuditable)) continue;

                var auditable = entry.Entity as IAuditable;
                if (entry.State == EntityState.Added)
                {
                  //  auditable.CreatedBy = UserProvider;//  
                    auditable.CreatedOn = TimestampProvider();
                    auditable.ModifiedOn = TimestampProvider();
                    continue;
                }

                   // auditable.UpdatedBy = UserProvider;
                    auditable.ModifiedOn = TimestampProvider();               
            }
        }
    }
}
