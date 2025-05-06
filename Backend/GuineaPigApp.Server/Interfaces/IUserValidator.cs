using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IUserValidator
    {
        void ThrowIfUserIsNull(User? user);
    }
}
