using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IProductSeederRepository
    {
        void AddListProduct(List<Product> products);
    }
}
