using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.ReferendumModelDTOs
{
    public class ReferendumIndexDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public string Name { get; set; }
        public int InstructionId { get; set; }
        public Instruction Instruction { get; set; }
        public int DecisionId { get; set; }
        public Decision Decision { get; set; }
        public List<Info> Infos { get; set; }
        public CalendarPlan CalendarPlan { get; set; }
        public int CalendarPlanId { get; set; }
    }
}
