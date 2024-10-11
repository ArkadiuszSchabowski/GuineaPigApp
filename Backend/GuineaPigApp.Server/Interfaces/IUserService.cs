using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IUserService
    {
        GetUserDto GetUser(string email);
        void UpdateUser(string email, UpdateUserDto dto);
    }
}
