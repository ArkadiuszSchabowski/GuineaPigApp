using GuineaPigApp.Server.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace GuineaPigApp.Server.Interfaces
{
    public interface ILoginValidator
    {
        void ThrowIfInvalidLogin(User? user);
        void ThrowIfInvalidLogin(PasswordVerificationResult password);
        void ThrowIfInvalidPassword(PasswordVerificationResult result);
        void ThrowIfPasswordsDoNotMatch(string password, string repeatPassword);
    }
}
