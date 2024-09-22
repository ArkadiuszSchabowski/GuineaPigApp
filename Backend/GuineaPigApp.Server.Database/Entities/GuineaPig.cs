using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Database.Entities
{
    public class GuineaPig
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Imię świnki nie może być krótsze niż 3 znaki!")]
        [MaxLength(25, ErrorMessage = "Imię świnki nie może być dłuższe niż 25 znaków!")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(50, 3000, ErrorMessage = "Waga świnki powinna wynosić od 50 do 3000 gram!")]
        public int Weight { get; set; }

        public virtual User User { get; set; } = new User();
        [Required]
        public int UserId { get; set; }
        public List<GuineaPigWeight> GuineaPigWeights { get; set; } = new List<GuineaPigWeight>();
    }

}
