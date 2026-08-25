using System.ComponentModel.DataAnnotations;

namespace APIconvenios.DTOs.Usuario
{
    public class ResetPasswordDto
    {
        [Required]
        public string NewPassword { get; set; }
    }
}
