using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class Candidate:BaseEntity
    {
        public string FullName { get; set; }
        public int VotedCount { get; set; } 
        public string About { get; set; }
        public string Party { get; set; }
         public double VotedPercent { get; set; }
        public string ImageUrl { get; set; }
    }
}
