using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IProductService
    {
        ProductResultDto GetBadProductsResult(PaginationDto dto);
        ProductResultDto GetGoodProductsResult(PaginationDto dto);
        void RemoveProduct(int id);
    }
}
