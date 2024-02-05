using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class Accredation :BaseEntity
    {
        public string Name { get;set; }
        public string PDFUrlEn { get; set; }
        public string PDFUrlRu { get; set; }
        public string PDFUrlAz { get; set; }

    }
}
