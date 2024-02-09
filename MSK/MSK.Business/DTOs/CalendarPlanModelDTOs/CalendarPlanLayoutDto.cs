using Microsoft.AspNetCore.Http;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.CalendarPlanModelDTOs
{
    public class CalendarPlanLayoutDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PdfUrl { get; set; }
        public int? ReferendumId { get; set; }
        public Referendum? Referendum { get; set; }



    }
}
