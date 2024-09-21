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
            _repository.SaveChanges();
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
            var product = _repository.GetProduct(id);

            if (product == null)
            {
                throw new BadRequestException("Nie znaleziono produktu o podanym Id!");
            }

            return product;

        }

        public void RemoveProduct(int id)
        {
            var product = _repository.GetProduct(id);

            if (product == null)
            {
                throw new BadRequestException("Nie znaleziono produktu o podanym Id!");
            }

            _repository.RemoveProduct(product);
            _repository.SaveChanges();
        }
    }
}