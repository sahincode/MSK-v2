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
    public class HomeSlideConfiguration : IEntityTypeConfiguration<HomeSlide>
    {
        public void Configure(EntityTypeBuilder<HomeSlide> builder)
        {
            builder.Property(c => c.Title).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(300);
            builder.Property(c => c.SubDescription).IsRequired().HasMaxLength(200);
            builder.Property(c => c.ImageUrl).IsRequired().HasMaxLength(100);


        }
    }
}
