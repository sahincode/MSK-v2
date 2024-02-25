using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSK.Core.Models;

namespace MSK.Data.Configurations
{
    public class ElectionConfiguration : IEntityTypeConfiguration<Election>
    {
        public void Configure(EntityTypeBuilder<Election> builder)
        {
            builder.Property(r => r.Name).IsRequired().HasMaxLength(200);
            builder.HasOne(r => r.CalendarPlan).WithOne(c => c.Election).HasForeignKey<CalendarPlan>(cp => cp.ElectionId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(r => r.Decision).WithOne(d => d.Election).HasForeignKey<Decision>(cp => cp.ElectionId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(r => r.Instruction).WithOne(i => i.Election).HasForeignKey<Instruction>(cp => cp.ElectionId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
