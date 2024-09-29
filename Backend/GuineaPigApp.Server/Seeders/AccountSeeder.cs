using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GuineaPigApp.Server.Seeders
{
    public class AccountSeeder : IAccountSeeder
    {
        private readonly MyDbContext _context;
        private readonly IPasswordHasher<User> _hasher;

        public AccountSeeder(MyDbContext context, IPasswordHasher<User> hasher)
        {
            _context = context;
            _hasher = hasher;
        }
        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public void SeedData()
        {
            var userPassword = "User123";
            var managerPassword = "Manager123";
            var adminPassword = "Admin123";

            if (_context.Database.CanConnect())
            {
                if (!_context.Users.Any())
                {

                    var user = new User()
                    {
                        Email = "user@gmail.com",
                        Name = "Damian",
                        Surname = "Kowalski",
                        City = "Warszawa",
                        RoleId = 1,
                    };
                    user.PasswordHash = HashPassword(user, userPassword);

                    var manager = new User()
                    {
                        Email = "manager@gmail.com",
                        Name = "Dominika",
                        Surname = "Nowak",
                        City = "Kraków",
                        RoleId = 2,
                    };
                    manager.PasswordHash = HashPassword(manager, managerPassword);

                    var admin = new User()
                    {
                        Email = "admin@gmail.com",
                        Name = "Katarzyna",
                        Surname = "Rajek",
                        City = "Wrocław",
                        RoleId = 3,
                    };
                    admin.PasswordHash = HashPassword(admin, adminPassword);

                    _context.Users.AddRange(user, manager, admin);
                    _context.SaveChanges();
                }
            }
        }
    }
}
