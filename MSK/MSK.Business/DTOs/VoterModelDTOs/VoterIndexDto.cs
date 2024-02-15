using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.VoterModelDTOs
{
    public class VoterIndexDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string FinCode { get; set; }
        public DateTime BirthOfDate { get; set; }
        public string ImageUrl { get; set; }

    }
}
