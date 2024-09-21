using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IProductService
    {
        void AddProduct(ProductDto dto);
        List<Product> GetBadProducts();
        List<Product> GetGoodProducts();
        Product GetProduct(int id);
        void RemoveProduct(int id);
    }
}
