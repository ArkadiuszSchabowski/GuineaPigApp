namespace GuineaPigApp.Server.Database.Entities
{
    public class GuineaPig
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Weight { get; set; }

        public virtual User User { get; set; } = new User();
        public int UserId { get; set; }
        public List<GuineaPigWeight> GuineaPigWeights { get; set; } = new List<GuineaPigWeight>();
    }
}
