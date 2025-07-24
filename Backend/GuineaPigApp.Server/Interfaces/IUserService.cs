using GuineaPigApp_Server.Models.Add;
using GuineaPigApp_Server.Models.Get;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IUserService
    {
        GetUserDto GetUser(string email);
        List<GetUserDto> GetUsers();
        void UpdateUser(string email, UpdateUserDto dto);
    }
}
