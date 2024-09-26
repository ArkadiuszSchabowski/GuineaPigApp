using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IGuineaPigRepository
    {
        void AddGuineaPig(GuineaPig guineaPig);
        bool PigExists(User user, string guineaPigName);
    }
}
