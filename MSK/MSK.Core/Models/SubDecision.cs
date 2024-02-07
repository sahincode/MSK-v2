using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class SubDecision :BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public Decision Decision { get; set; }
        public int DecisionId { get; set; }

    }
}
