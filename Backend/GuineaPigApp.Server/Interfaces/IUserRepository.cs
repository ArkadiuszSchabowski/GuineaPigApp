using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IUserRepository
    {
        User? GetUser(string email);
        void AddUser(User user);
        void RemoveUser(User user);
        void UpdateUser(User user);
        void SaveChanges();
        List<User> GetUsers();
    }
}
