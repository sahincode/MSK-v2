using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get;set; }
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        public bool Succeeded { get; set; }

    }
}
