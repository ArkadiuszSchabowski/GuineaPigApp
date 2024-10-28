using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Database.Entities
{
    public class GuineaPigWeight
    {
        public int Id { get; set; }

        [Required]
        [Range(50, 3000, ErrorMessage = "Waga świnki powinna wynosić od 50 do 3000 gram!")]
        public int Weight { get; set; }
        public DateOnly Date { get; set; }

        [Required]
        public int GuineaPigId { get; set; }
        public GuineaPig GuineaPig { get; set; } = new GuineaPig();
    }
}
