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
    public class CalendarPlanConfiguration : IEntityTypeConfiguration<CalendarPlan>
    {
        public void Configure(EntityTypeBuilder<CalendarPlan> builder)
        {
            builder.Property(c => c.Title).IsRequired().HasMaxLength(1000);
            builder.Property(c => c.PdfUrl).IsRequired().HasMaxLength(100);
           
        }
    }
}
