using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductValidator _validator;

        public ProductService(IProductRepository repository, IProductValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public void AddProduct(ProductDto dto)
        {
            _validator.ValidateName(dto.Name);

            var product = new Product();

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.ImageUrl = dto.ImageUrl;
            product.isGoodProduct = dto.isGoodProduct;

            _repository.AddProduct(product);
        }

        public List<Product> GetBadProducts()
        {
            var badProducts = _repository.GetBadProducts();

            return badProducts;
        }

        public List<Product> GetGoodProducts()
        {
            var goodProducts = _repository.GetGoodProducts();

            return goodProducts;
        }

        public Product GetProduct(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("Wartość Id musi być większa od 0");
            }

            var product = _repository.GetProduct(id);

            if (product == null)
            {
                throw new BadRequestException("Nie znaleziono produktu o podanym Id!");
            }

            return product;
        }

        public void RemoveProduct(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wartość Id musi być większa od 0");
            }

            var product = _repository.GetProduct(id);

            if (product == null)
            {
                throw new BadRequestException("Nie znaleziono produktu o podanym Id!");
            }

            _repository.RemoveProduct(product);
        }
    }
}