using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Core.Models
{
    public class Voter :BaseEntity
    { 
        public string FullName { get;set; }
        public string Address { get; set; }
        public string FinCode { get; set; }
        public DateTime BirthOfDate { get; set; }
        public  string ImageUrl { get; set; }
    }
}
