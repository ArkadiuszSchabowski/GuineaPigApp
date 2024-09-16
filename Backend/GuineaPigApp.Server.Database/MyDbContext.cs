using Microsoft.EntityFrameworkCore;

namespace GuineaPigApp.Server.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }
    }
}
