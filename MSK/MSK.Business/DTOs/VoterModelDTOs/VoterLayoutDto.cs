using Microsoft.AspNetCore.Http;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.VoterModelDTOs
{
    public class VoterLayoutDto
    {
        public string  Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string FinCode { get; set; }
        public DateTime BirthOfDate { get; set; }
        public string ImageUrl { get; set; }


    }
}
