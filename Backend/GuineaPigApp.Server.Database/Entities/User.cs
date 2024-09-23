using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GuineaPigApp.Server.Database.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Długość imienia nie może być krótsza niż 3 znaki!")]
        [MaxLength(25, ErrorMessage = "Długość imienia nie może być dłuższa niż 25 znaków!")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Długość nazwiska nie może być krótsza niż 3 znaki!")]
        [MaxLength(25, ErrorMessage = "Długość nazwiska nie może być dłuższa niż 25 znaków!")]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Długość miasta nie może być krótsza niż 3 znaki!")]
        [MaxLength(25, ErrorMessage = "Długość miasta nie może być dłuższa niż 25 znaków!")]
        public string City { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
        public List<GuineaPig> GuineaPig { get; set; } = new List<GuineaPig>();
    }
}