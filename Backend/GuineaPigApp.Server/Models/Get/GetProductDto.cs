namespace GuineaPigApp_Server.Models.Get
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "/assets/images/products/default.jpg";
        public bool IsGoodProduct { get; set; }
    }
}
