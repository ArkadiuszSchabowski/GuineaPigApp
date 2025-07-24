using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp_Server.Models.Add
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage ="Niepoprawny format adresu email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string Password { get; set; } = string.Empty;
    }
}