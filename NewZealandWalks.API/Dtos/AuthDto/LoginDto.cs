using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.API.Dtos.AuthDto
{
    public class LoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
