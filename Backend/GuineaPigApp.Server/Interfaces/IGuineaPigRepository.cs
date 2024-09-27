using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IGuineaPigRepository
    {
        void AddGuineaPig(GuineaPig guineaPig);
        GuineaPig? GetGuineaPig(int userId, string guineaPigName);
        List<GuineaPig> GetGuineaPigs(User user);
        bool PigExists(User user, string guineaPigName);
    }
}
