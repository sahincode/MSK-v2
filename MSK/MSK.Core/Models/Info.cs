using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class Info :BaseEntity
    {
        public string Name { get; set; }
        public string PdfUrl { get;set; }
        public Referendum? Referendum { get; set; }
        public int? ReferendumId { get; set; }
    }
}
