using GuineaPigApp.Server.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuineaPigApp.Server.Database
{
    public class MyDbContext : DbContext
    {
        public DbSet<Product> BadProducts { get; set; }
        public DbSet<Product> GoodProducts { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }
    }
}
