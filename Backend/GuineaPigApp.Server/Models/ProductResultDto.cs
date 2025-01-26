namespace GuineaPigApp.Server.Models
{
    public class ProductResultDto
    {
        public List<GetProductDto> Products { get; set; } = new List<GetProductDto>();
        public int TotalCount { get; set; }
    }
}