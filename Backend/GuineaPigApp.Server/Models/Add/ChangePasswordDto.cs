using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp_Server.Models.Add
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Aktualne hasło jest wymagane.")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nowe hasło jest wymagane.")]
        [MinLength(5, ErrorMessage = "Nowe hasło nie może być krótsze niż 5 znaków.")]
        [MaxLength(25, ErrorMessage = "Nowe hasło nie może być dłuższe niż 25 znaków.")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Powtórzenie hasła jest wymagane.")]
        [MinLength(5, ErrorMessage = "Powtórzone hasło musi mieć minimum 5 znaków.")]
        [MaxLength(25, ErrorMessage = "Powtórzone hasło może mieć maksymalnie 25 znaków.")]
        public string RepeatNewPassword { get; set; } = string.Empty;
    }
}
