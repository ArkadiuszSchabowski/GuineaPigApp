using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IUserService
    {
        GetUserDto GetUser(string email);
    }
}
