using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Database.Entities
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Nazwa roli nie może być krótsza niż 3 znaki!")]
        [MaxLength(25, ErrorMessage = "Nazwa roli nie może być dłuższa niż 25 znaków!")]
        public string Name { get; set; } = string.Empty;
        public List<User> Users { get; set; } = new List<User>();
    }
}
