using System.ComponentModel.DataAnnotations;

namespace ClaimBasedAuth.Models
{
    public class UserLoginModel
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
