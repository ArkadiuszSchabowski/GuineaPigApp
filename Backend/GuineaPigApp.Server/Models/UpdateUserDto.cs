using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Models
{
    public class UpdateUserDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Długość imienia nie może być krótsza niż 3 znaki!")]
        [MaxLength(25, ErrorMessage = "Długość imienia nie może być dłuższa niż 25 znaków!")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Długość nazwiska nie może być krótsza niż 3 znaki!")]
        [MaxLength(25, ErrorMessage = "Długość nazwiska nie może być dłuższa niż 25 znaków!")]
        public string Surname { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Długość nazwy miasta nie może być krótsza niż 3 znaki!")]
        [MaxLength(25, ErrorMessage = "Długość nazwy miasta nie może być dłuższa niż 25 znaków!")]
        public string City { get; set; } = string.Empty;
    }
}
