using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IGuineaPigService
    {
        void AddGuineaPig(string email, GuineaPigDto dto);
        GuineaPigDto GetGuineaPig(string email, string name);
        List<GuineaPigDto> GetGuineaPigs(string email);
        void RemoveGuineaPig(string email, string guineaPigName);
    }
}
