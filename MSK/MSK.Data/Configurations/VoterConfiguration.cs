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
    public class VoterConfiguration : IEntityTypeConfiguration<Voter>
    {
        public void Configure(EntityTypeBuilder<Voter> builder)
        {
            builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(500);
            builder.Property(c => c.FinCode).IsRequired().HasMaxLength(7);
            builder.Property(c => c.ImageUrl).IsRequired().HasMaxLength(100);

        }
    }
}
