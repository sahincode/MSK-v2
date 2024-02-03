using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class PressNew :BaseEntity
    {
        public string Title { get; set; }
        public  string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Article { get; set; }
    }
}
