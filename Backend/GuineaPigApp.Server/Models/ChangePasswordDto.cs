using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Models
{
    public class ChangePasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Hasło nie może być krótsze niż 5 znaków")]
        [MaxLength(25, ErrorMessage = "Hasło nie może być dłuższe niż 25 znaków!")]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Hasło nie może być krótsze niż 5 znaków")]
        [MaxLength(25, ErrorMessage = "Hasło nie może być dłuższe niż 25 znaków!")]
        public string RepeatNewPassword { get; set; } = string.Empty;
    }
}
