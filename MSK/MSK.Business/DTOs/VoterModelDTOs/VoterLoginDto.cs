using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs.VoterModelDTOs
{
    public class VoterLoginDto
    {
        public string FinCode { get; set; }
        public IFormFile Image { get; set; }
    }
}
