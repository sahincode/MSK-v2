using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.SubInstructionModelDTOs
{
    public class SubInstructionIndexDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
