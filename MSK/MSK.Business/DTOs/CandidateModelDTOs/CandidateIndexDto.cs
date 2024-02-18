using Microsoft.AspNetCore.Http;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.CandidateModelDTOs
{
    public class CandidateIndexDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public string FullName { get; set; }
        public string Profession { get; set; }
        public int VotedCount { get; set; }
        public string About { get; set; }
        public string Party { get; set; }
        public double VotedPercent { get; set; }
        public string ImageUrl { get; set; }
    }
}
