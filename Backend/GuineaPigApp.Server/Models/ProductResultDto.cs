namespace GuineaPigApp.Server.Models
{
    public class ProductResultDto
    {
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public int TotalCount { get; set; }
    }
}