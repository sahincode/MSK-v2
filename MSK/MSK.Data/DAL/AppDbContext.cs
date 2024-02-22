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
        public DbSet<PressNew> PressNews { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<NationalAttribute> NationalAttributes { get; set; }
        public DbSet<Accredation> Accredations { get; set; }
        public DbSet<Legislation> Legislations { get; set; }
        public DbSet<Decision> Decisions { get; set; }
        public DbSet<SubDecision> SubDecisions { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<SubInstruction> SubInstructions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Info> Infos{ get; set; }
        public DbSet<Referendum> Referendums { get; set; }
        public DbSet<CalendarPlan> CalendarPlans { get; set; }
        public DbSet<Voter>  Voters { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Chat> Chats { get; set; }




        public AppDbContext(DbContextOptions options):base(options){}
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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
                            entity.CreationTime = DateTime.UtcNow.AddHours(4);
                            break;
                        case EntityState.Modified:
                            entity.UpdateTime = DateTime.UtcNow.AddHours(4);

                            break;
                        
                        case EntityState.Detached:
                            entity.UpdateTime = DateTime.UtcNow.AddHours(4);

                            break;
                        case EntityState.Unchanged:
                            break;

                    }
                }
            }
            return await  base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(SettingConfiguration).Assembly);
            base.OnModelCreating(builder);
        }

    }
}
