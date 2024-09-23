using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        List<Product> GetBadProducts();
        List<Product> GetGoodProducts();
        Product? GetProduct(int id);
        void RemoveProduct(Product product);
    }
}
