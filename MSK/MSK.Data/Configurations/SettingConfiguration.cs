using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSK.Core.Models;

namespace MSK.Data.Configirations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(c => c.Key).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Value).IsRequired().HasMaxLength(100);
        }
    }
}
