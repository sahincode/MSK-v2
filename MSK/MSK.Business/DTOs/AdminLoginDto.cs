using System.ComponentModel.DataAnnotations;

namespace MSK.Business.DTOs
{
    public class AdminLoginDto
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; }
    }
}
