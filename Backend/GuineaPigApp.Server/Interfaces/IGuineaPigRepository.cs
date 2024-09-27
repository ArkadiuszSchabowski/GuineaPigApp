using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IGuineaPigRepository
    {
        void AddGuineaPig(GuineaPig guineaPig);
        List<GuineaPig> GetGuineaPigs(User user);
        bool PigExists(User user, string guineaPigName);
    }
}
