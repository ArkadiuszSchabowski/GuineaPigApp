using GuineaPigApp.Server.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuineaPigApp.Server.Database
{
    public class MyDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<GuineaPig> GuineaPigs { get; set; }
        public DbSet<GuineaPigWeight> GuineaPigWeights { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "User"
                },
                new Role
                {
                    Id = 2,
                    Name = "Manager"
                },
                new Role
                {
                    Id = 3,
                    Name = "Admin"
                });

            modelBuilder.Entity<Role>()
                .HasMany(u => u.Users)
                .WithOne(r => r.Role)
                .HasForeignKey(r => r.RoleId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.GuineaPig)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);

            modelBuilder.Entity<GuineaPig>()
                .HasMany(g => g.GuineaPigWeights)
                .WithOne(p => p.GuineaPig)
                .HasForeignKey(p => p.GuineaPigId);
        }
    }
}
