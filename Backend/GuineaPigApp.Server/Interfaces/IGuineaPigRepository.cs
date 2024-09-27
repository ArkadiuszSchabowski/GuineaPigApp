using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IGuineaPigRepository
    {
        void AddGuineaPig(GuineaPig guineaPig);
        List<GuineaPig> GetGuineaPigs(int userId);
        bool PigExists(User user, string guineaPigName);
    }
}
