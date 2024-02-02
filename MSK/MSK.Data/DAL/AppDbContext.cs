using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MSK.Core.Models;
using MSK.Data.Configirations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Data.DAL
{
    public class AppDbContext :IdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<HomeSlide> HomeSlides { get; set; }


        public AppDbContext(DbContextOptions options):base(options){}
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                var entity = entry.Entity;

                if (entity != null)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = DateTime.UtcNow.AddHours(4);
                            break;
                        case EntityState.Modified:
                            entity.UpdateDate = DateTime.UtcNow.AddHours(4);

                            break;
                        case EntityState.Deleted:
                            entity.UpdateDate = DateTime.UtcNow.AddHours(4);

                            break;
                        case EntityState.Detached:
                            entity.UpdateDate = DateTime.UtcNow.AddHours(4);

                            break;
                        case EntityState.Unchanged:
                            break;

                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(SettingConfiguration).Assembly);
            base.OnModelCreating(builder);
        }

    }
}
