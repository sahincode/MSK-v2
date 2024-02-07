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
    public class SubinstructionConfiguration : IEntityTypeConfiguration<SubInstruction>
    {
        public void Configure(EntityTypeBuilder<SubInstruction> builder)
        {
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Url).IsRequired().HasMaxLength(100);
            builder.HasOne(c => c.Instruction).WithMany(d => d.SubInstructions);

        }
    }
}
