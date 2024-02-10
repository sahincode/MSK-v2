using Microsoft.AspNetCore.Http;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.InfoModelDTOs
{
    public class InfoLayoutDto
    {
        public DateTime CreationTime { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PdfUrl { get; set; }
        public Referendum Referendum { get; set; }


    }
}
