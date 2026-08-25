using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
