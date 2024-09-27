using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IGuineaPigService
    {
        void AddGuineaPig(string email, GuineaPigDto dto);
        GuineaPigDto GetGuineaPig(string email, string name);
        List<GuineaPig> GetGuineaPigs(string email);
        void RemoveGuineaPig(string email, string name);
    }
}
