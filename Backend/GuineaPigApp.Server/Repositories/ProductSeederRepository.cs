using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
namespace GuineaPigApp.Server.Repositories
{
    public class ProductSeederRepository : IProductSeederRepository
    {
        private readonly MyDbContext _context;

        public ProductSeederRepository(MyDbContext context)
        {
            _context = context;
        }
        public void AddListProduct(List<Product> products)
        {
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }
    }
}
