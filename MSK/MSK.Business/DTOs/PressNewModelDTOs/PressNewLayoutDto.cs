using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.PressNewDTOs
{
    public class PressNewLayoutDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Article { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
