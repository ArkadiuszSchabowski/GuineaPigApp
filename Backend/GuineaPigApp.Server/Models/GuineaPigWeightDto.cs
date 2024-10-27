using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Models
{
    public class GuineaPigWeightDto
    {
        [Required]
        [Range(50, 3000, ErrorMessage = "Waga świnki powinna wynosić od 50 do 3000 gram!")]
        public int Weight { get; set; }
    }
}
