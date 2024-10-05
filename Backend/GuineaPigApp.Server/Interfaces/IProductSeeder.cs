using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IProductSeeder
    {
        List<ProductDto> GetBadProducts();
        List<ProductDto> GetGoodProducts();
        List<Product> GetProducts(List<ProductDto> dto);
        void SeedData();
    }
}
