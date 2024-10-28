namespace GuineaPigApp.Server.Models
{
    public class GuineaPigWeightsDto
    {
        public int Weight { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}