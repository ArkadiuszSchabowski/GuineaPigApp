using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<Product> GetBadProducts(PaginationDto dto)
        {
            return _context.Products
                     .Where(x => x.IsGoodProduct == false)
                     .OrderBy(x => x.Name)
                     .Skip((dto.PageNumber - 1) * dto.PageSize)
                     .Take(dto.PageSize)
                     .ToList();
        }
        public List<Product> GetGoodProducts(PaginationDto dto)
        {
            return _context.Products
           .Where(x => x.IsGoodProduct == true)
           .OrderBy(x => x.Name)
           .Skip((dto.PageNumber - 1) * dto.PageSize)
           .Take(dto.PageSize)
           .ToList();
        }
        public Product? GetProduct(int id)
        {
            return _context.Products.SingleOrDefault(x => x.Id == id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public Product? EnsureProductDoesNotExist(string name)
        {
            var product = _context.Products.SingleOrDefault(x => x.Name == name);

            return product;
        }
    }
}
