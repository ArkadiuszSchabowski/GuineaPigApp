using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IProductService
    {
        void AddProduct(ProductDto dto);
        List<Product> GetBadProducts(PaginationDto dto);
        List<Product> GetGoodProducts(PaginationDto dto);
        Product GetProduct(int id);
        void RemoveProduct(int id);
    }
}
