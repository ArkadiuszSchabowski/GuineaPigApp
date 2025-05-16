using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IUserValidator
    {
        void ThrowIfUserIsNull(User? user);
        void ThrowIfUserExist(User? user);
        void ThrowIfRemovingDefaultAccount(string email);
        void ThrowIfUserRoleIsNull(Role? role);
    }
}
