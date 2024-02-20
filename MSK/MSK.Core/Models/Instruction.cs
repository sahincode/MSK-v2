using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class Instruction : BaseEntity
    {
        public string Name { get; set; }
        public List<SubInstruction> SubInstructions { get; set; }
        public Referendum? Referendum { get; set; }
        public int? ReferendumId { get; set; }
        public Election? Election { get; set; }
        public int? ElectionId { get; set; }
    }
}
