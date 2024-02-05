using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.AccredationModelDTOs
{
    public class AccredationLayoutDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PDFUrlEn { get; set; }
        public string PDFUrlRu { get; set; }
        public string PDFUrlAz { get; set; }
    }
}
