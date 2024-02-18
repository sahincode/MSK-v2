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
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Profession).IsRequired().HasMaxLength(100);

            builder.Property(c => c.About).IsRequired().HasMaxLength(1000);
            builder.Property(c => c.Party).IsRequired().HasMaxLength(100);
            builder.Property(c => c.ImageUrl).IsRequired().HasMaxLength(100);
        }
    }
}
