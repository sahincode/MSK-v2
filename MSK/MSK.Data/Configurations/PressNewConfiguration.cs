using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Data.Configurations
{
    public class PressNewConfiguration : IEntityTypeConfiguration<PressNew>
    {
        public void Configure(EntityTypeBuilder<PressNew> builder)
        {
            builder.Property(c => c.Title).IsRequired().HasMaxLength(100);
            builder.Property(c => c.ImageUrl).IsRequired().HasMaxLength(100);
            
            builder.Property(c => c.Description).IsRequired().HasMaxLength(300);
            builder.Property(c => c.Article).IsRequired().HasMaxLength(2000);

        }
    }
}
