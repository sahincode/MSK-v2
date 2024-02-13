using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs
{
    public class RegisterModelDto
    {
        public string FullName { get; set; }
      
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password" ,ErrorMessage ="Please add same password!")]
        public string ConfirmPassword { get; set; }
    }
}
