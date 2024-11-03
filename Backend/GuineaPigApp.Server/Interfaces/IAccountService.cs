using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IAccountService
    {
        void ChangePassword(ChangePasswordDto dto);
        string GenerateJWT(LoginUserDto loginUserDto);
        void RegisterUser(RegisterUserDto registerUserDto);
        void DeleteAccount(string email);
        void CheckEmail(string email);
    }
}
