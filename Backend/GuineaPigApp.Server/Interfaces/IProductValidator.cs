using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IProductValidator
    {
        void ThrowIfProductExist(Product? product);
        void ThrowIfProductIsNotCorrect(ProductDto productDto);
        void ThrowIfProductIsNull(Product? product);
    }
}
