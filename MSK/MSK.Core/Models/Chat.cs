using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class Chat:BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string ChatterId { get; set; }

    }
}
