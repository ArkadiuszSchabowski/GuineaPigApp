using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IGuineaPigRepository
    {
        void Add(GuineaPig guineaPig);
        void AddGuineaPigWeight(GuineaPigWeight guineaPigWeight);
        GuineaPig? GetGuineaPig(int userId, string guineaPigName);
        List<GuineaPig> GetGuineaPigs(int userId);
        List<GuineaPigWeight> GetWeights(GuineaPig guineaPig);
        bool PigExists(User user, string guineaPigName);
        void RemoveGuineaPig(GuineaPig guineaPig);
    }
}
