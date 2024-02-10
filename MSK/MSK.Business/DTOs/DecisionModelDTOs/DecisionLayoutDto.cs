using Microsoft.AspNetCore.Http;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.DecisionModelDTOs
{
    public class DecisionLayoutDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SubDecision> SubDecisions { get; set; }

    }
}
