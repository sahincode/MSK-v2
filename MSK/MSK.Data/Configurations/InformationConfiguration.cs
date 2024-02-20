using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Data.Configurations
{
    public class InformationConfiguration : IEntityTypeConfiguration<Info>
    {
        public void Configure(EntityTypeBuilder<Info> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(1000);
          


            builder.Property(c => c.PdfUrl).IsRequired().HasMaxLength(100);
            builder.HasOne(c => c.Referendum).WithMany(r => r.Infos);
            builder.HasOne(c => c.Election).WithMany(r => r.Infos);

        }
    }
}
