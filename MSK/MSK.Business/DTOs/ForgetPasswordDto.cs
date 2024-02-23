using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs
{
    public class ForgetPasswordDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool EmailSent { get; set; } 
    }
}
