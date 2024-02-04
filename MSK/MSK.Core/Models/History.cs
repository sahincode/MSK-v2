using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class History : BaseEntity
    {
        public string InfoStart { get; set; }
        public string InfoMiddle { get; set; }

        public string InfoEnd { get; set; }
    }
}

