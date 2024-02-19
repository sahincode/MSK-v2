using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class Vote :BaseEntity
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public string VoterFinCode { get; set; }
       

    }
}
