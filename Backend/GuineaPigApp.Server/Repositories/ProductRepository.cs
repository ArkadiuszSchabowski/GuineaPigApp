using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;

namespace GuineaPigApp.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<Product> GetBadProducts()
        {
            return _context.Products
                     .Where(x => x.isGoodProduct == false)
                     .ToList();
        }
        public List<Product> GetGoodProducts()
        {
            return _context.Products
         .Where(x => x.isGoodProduct == true)
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
