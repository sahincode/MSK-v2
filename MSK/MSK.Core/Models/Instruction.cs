using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class Instruction :BaseEntity
    {
        public string Name { get; set; }
        public List<SubInstruction> SubInstructions { get; set; }
    }
}
