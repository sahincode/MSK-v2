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
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.Property(c => c.Question).IsRequired().HasMaxLength(3000);
            builder.Property(c => c.Answer).IsRequired().HasColumnType("varchar").HasMaxLength(8000);
            builder.Property(c => c.ChatterId).IsRequired().HasMaxLength(7);


        }
    }
}
