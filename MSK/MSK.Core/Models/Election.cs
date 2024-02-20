using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public  class Election :BaseEntity
    {
        public string Name { get; set; }
        public int InstructionId { get; set; }
        public int VotersCount { get; set; }
        public Instruction Instruction { get; set; }
        public int DecisionId { get; set; }
        public Decision Decision { get; set; }
        public List<Info> Infos { get; set; }
        public CalendarPlan CalendarPlan { get; set; }
        public int CalendarPlanId { get; set; }
        public List<Candidate> Candidates { get; set; }
        public DateTime StartDate { get; set; }
    }
}
