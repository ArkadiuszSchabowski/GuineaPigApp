using System.ComponentModel.DataAnnotations;

namespace GuineaPigApp.Server.Database.Entities
{
    public class GuineaPigWeight
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public DateOnly Date { get; set; }
        public int GuineaPigId { get; set; }
        public GuineaPig GuineaPig { get; set; } = new GuineaPig();
    }
}
