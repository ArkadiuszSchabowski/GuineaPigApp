using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly MyDbContext _context;

        public ProductService(MyDbContext context)
        {
            _context = context;
        }

        public void AddProduct(ProductDto dto)
        {
            var product = new Product();

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.ImageUrl = dto.ImageUrl;
            product.isGoodProduct = dto.isGoodProduct;

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public List<Product> GetBadProducts()
        {
            var badProducts = _context.Products
                     .Where(x => x.isGoodProduct == false)
                     .ToList();

            return badProducts;
        }

        public List<Product> GetGoodProducts()
        {
            var goodProducts = _context.Products
         .Where(x => x.isGoodProduct == true)
         .ToList();

            return goodProducts;
        }

        public Product GetProduct(int id)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                throw new BadRequestException("Nie znaleziono produktu o podanym Id!");
            }

            return product;

        }

        public void RemoveProduct(int id)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                throw new BadRequestException("Nie znaleziono produktu o podanym Id!");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
