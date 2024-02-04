using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.HistoryModelDTOs
{
    public class HistoryIndexDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime DeletedTime { get; set; }
           public string InfoStart { get; set; }
        public string InfoMiddle { get; set; }

        public string InfoEnd { get; set; }
    }
}
