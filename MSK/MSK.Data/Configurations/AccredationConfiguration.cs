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
    public class AccredationConfiguration : IEntityTypeConfiguration<Accredation>
    {
        public void Configure(EntityTypeBuilder<Accredation> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.PDFUrlEn).IsRequired().HasMaxLength(100);
            builder.Property(c => c.PDFUrlRu).IsRequired().HasMaxLength(100);
            builder.Property(c => c.PDFUrlAz).IsRequired().HasMaxLength(100);

        }
    }
}
