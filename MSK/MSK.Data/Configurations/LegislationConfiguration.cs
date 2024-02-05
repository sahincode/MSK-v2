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
    public class LegislationConfiguration : IEntityTypeConfiguration<Legislation>
    {
        public void Configure(EntityTypeBuilder<Legislation> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.PdfUrl).IsRequired().HasMaxLength(100);
        }
    }
}
