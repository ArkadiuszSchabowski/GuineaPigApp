using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuineaPigApp.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;

        public UserRepository(MyDbContext context)
        {
            _context = context;
        }
        public User? GetUser(string email)
        {
            var user = _context.Users.Include(x => x.Role).
                FirstOrDefault(x => x.Email == email);

            return user;
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
