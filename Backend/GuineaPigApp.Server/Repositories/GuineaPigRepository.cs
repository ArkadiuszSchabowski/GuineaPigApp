using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
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
        public void AddGuineaPigWeight(GuineaPigWeight guineaPigWeight)
        {
            _context.GuineaPigWeights.Add(guineaPigWeight);
            _context.SaveChanges();
        }
        public GuineaPig? GetGuineaPig(int userId, string guineaPigName)
        {
            GuineaPig? guineaPig = _context.GuineaPigs.SingleOrDefault(g => g.UserId == userId && g.Name == guineaPigName);

            return guineaPig;
        }

        public List<GuineaPig> GetGuineaPigs(int userId)
        {
            return _context.GuineaPigs.Where(g => g.UserId == userId).ToList();
        }

        public bool PigExists(User user, string name)
        {
            bool guineaPig = _context.Users.Include(x => x.GuineaPig)
                .Where(x => x.Id == user.Id)
                .Any(x => x.GuineaPig.Any(x => x.Name == name));

            return guineaPig;
        }

        public void RemoveGuineaPig(GuineaPig guineaPig)
        {
            _context.GuineaPigs.Remove(guineaPig);
            _context.SaveChanges();
        }
    }
}
