using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class GuineaPigService : IGuineaPigService
    {
        public void AddGuineaPig(string email, GuineaPigDto dto)
        {
            throw new NotImplementedException();
        }

        public GuineaPigDto GetGuineaPig(string email, string name)
        {
            throw new NotImplementedException();
        }

        public List<GuineaPigDto> GetGuineaPigs(string email)
        {
            throw new NotImplementedException();
        }

        public void RemoveGuineaPig(string email, string name)
        {
            throw new NotImplementedException();
        }
    }
}
