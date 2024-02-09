using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSK.Core.Models;

namespace MSK.Data.Configurations
{
    public class ReferendumConfiguration : IEntityTypeConfiguration<Referendum>
    {
        public void Configure(EntityTypeBuilder<Referendum> builder)
        {
            builder.Property(r => r.Name).IsRequired().HasMaxLength(200);
            builder.HasOne(r => r.CalendarPlan).WithOne(c => c.Referendum).HasForeignKey<CalendarPlan>(cp=>cp.ReferendumId).IsRequired(false);
            builder.HasOne(r=> r.Decision).WithOne(d => d.Referendum).HasForeignKey<Decision>(cp => cp.ReferendumId).IsRequired(false)
                ;
            builder.HasOne(r => r.Instruction).WithOne(i => i.Referendum).HasForeignKey<Instruction>(cp => cp.ReferendumId).IsRequired(false);
        }
    }
}
