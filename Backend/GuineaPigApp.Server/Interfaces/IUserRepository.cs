using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IUserRepository
    {
        User? GetUser(string email);
        void AddUser(User user);
        void RemoveUser(User user);
        void UpdateUser(User user);
    }
}
