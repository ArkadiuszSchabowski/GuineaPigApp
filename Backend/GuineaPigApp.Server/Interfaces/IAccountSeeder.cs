using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IAccountSeeder
    {
        string HashPassword(User user, string password);
        void SeedData();
    }
}
