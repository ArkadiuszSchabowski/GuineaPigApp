using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Models
{
    public class GuineaPigDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Imię świnki nie może być krótsze niż 3 znaki!")]
        [MaxLength(25, ErrorMessage = "Imię świnki nie może być dłuższe niż 25 znaków!")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(50, 3000, ErrorMessage = "Waga świnki powinna wynosić od 50 do 3000 gram!")]
        public int Weight { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString();
    }
}
