using System.ComponentModel.DataAnnotations;

namespace MSK.Business.DTOs
{
    public class LoginModelDto
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
