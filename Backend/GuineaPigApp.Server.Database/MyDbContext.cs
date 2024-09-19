using GuineaPigApp.Server.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuineaPigApp.Server.Database
{
    public class MyDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }
    }
}
