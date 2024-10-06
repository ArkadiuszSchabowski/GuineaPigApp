using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        Product? EnsureProductDoesNotExist(string name);
        List<Product> GetBadProducts(PaginationDto dto);
        List<Product> GetGoodProducts(PaginationDto dto);
        Product? GetProduct(int id);
        void RemoveProduct(Product product);
    }
}
