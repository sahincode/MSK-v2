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
    public class InstructionCinfiguration : IEntityTypeConfiguration<Instruction>
    {
        public void Configure(EntityTypeBuilder<Instruction> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);

        }
    }
}
