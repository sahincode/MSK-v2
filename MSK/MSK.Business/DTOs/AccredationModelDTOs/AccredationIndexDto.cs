using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.AccredationModelDTOs
{
    public class AccredationIndexDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public string Name { get; set; }
        public string PDFUrlEn { get; set; }
        public string PDFUrlRu { get; set; }
        public string PDFUrlAz { get; set; }
    }
}
