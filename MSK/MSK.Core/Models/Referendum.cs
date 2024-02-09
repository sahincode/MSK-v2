using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class Referendum : BaseEntity
    {
        public int InstructionId { get; set; }
        public Instruction Instruction { get; set; }
        public int DecisionId { get; set; }
        public Decision Decision { get; set; }
        public List<Info> Infos { get; set; }
        public CalendarPlan CalendarPlan { get; set; }
        public int CalendarPlanId { get; set; }

    }
}
