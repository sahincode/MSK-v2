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
    public class SubDecisionConfiguration : IEntityTypeConfiguration<SubDecision>
    {
        public void Configure(EntityTypeBuilder<SubDecision> builder)
        {
            builder.Property(c => c.Title).IsRequired().HasMaxLength(800);
            builder.Property(c => c.Url).IsRequired().HasMaxLength(100);
            builder.HasOne(c => c.Decision).WithMany(d => d.SubDecisions);


        }
    }
}
