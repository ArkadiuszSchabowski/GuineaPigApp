using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IProductService
    {
        void AddProduct(ProductDto dto);
        ProductResultDto GetBadProductsResult(PaginationDto dto);
        ProductResultDto GetGoodProductsResult(PaginationDto dto);
        Product GetProduct(int id);
        void RemoveProduct(int id);
    }
}
