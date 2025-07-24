using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp_Server.Models.Add
{
    public class RegisterStepOneDto
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [MinLength(5, ErrorMessage = "Hasło nie może być krótsze niż 5 znaków.")]
        [MaxLength(25, ErrorMessage = "Hasło nie może być dłuższe niż 25 znaków.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Powtórzenie hasła jest wymagane.")]
        [MinLength(5, ErrorMessage = "Powtórzone hasło musi mieć minimum 5 znaków.")]
        [MaxLength(25, ErrorMessage = "Powtórzone hasło może mieć maksymalnie 25 znaków.")]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne.")]
        public string RepeatPassword { get; set; } = string.Empty;
    }
}
