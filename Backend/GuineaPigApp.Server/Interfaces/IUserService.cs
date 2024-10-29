using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IUserService
    {
        GetUserDto GetUser(string email);
        List<GetUserDto> GetUsers();
        void UpdateUser(string email, UpdateUserDto dto);
    }
}
