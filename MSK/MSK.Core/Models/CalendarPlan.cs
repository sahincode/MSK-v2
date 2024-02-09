using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public  class CalendarPlan :BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PdfUrl { get; set; }
        public int? ReferendumId { get;set; }
        public Referendum? Referendum{ get; set; }
    }
}
