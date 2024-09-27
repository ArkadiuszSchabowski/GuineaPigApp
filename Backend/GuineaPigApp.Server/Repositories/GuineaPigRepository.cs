using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuineaPigApp.Server.Repositories
{
    public class GuineaPigRepository : IGuineaPigRepository
    {
        private readonly MyDbContext _context;

        public GuineaPigRepository(MyDbContext context)
        {
            _context = context;
        }

        public void AddGuineaPig(GuineaPig guineaPig)
        {
            _context.GuineaPigs.Add(guineaPig);
            _context.SaveChanges();
        }
        public GuineaPig? GetGuineaPig(int userId, string guineaPigName)
        {
            GuineaPig? guineaPig = _context.GuineaPigs.SingleOrDefault(g => g.UserId == userId && g.Name == guineaPigName);

            return guineaPig;
        }

        public List<GuineaPig> GetGuineaPigs(User user)
        {
            return user.GuineaPig.ToList();
        }


        public bool PigExists(User user, string name)
        {
            bool guineaPig = _context.Users.Include(x => x.GuineaPig)
                .Where(x => x.Id == user.Id)
                .Any(x => x.GuineaPig.Any(x => x.Name == name));

            return guineaPig;
        }

    }
}
