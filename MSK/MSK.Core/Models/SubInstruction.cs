using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class SubInstruction :BaseEntity
    {
        public string Title { get;set; }
        public string Url { get; set; }
        public Instruction Instruction { get; set; }
        public int InstructionId { get; set; }

    }
}
