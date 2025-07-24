using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GuineaPigApp.Server.Validators
{
    public class LoginValidator : ILoginValidator
    {
        public void ThrowIfInvalidLogin(User? user)
        {
            if (user == null)
            {
                throw new BadRequestException("Błędne dane logowania.");
            }
        }

        public void ThrowIfInvalidLogin(PasswordVerificationResult password)
        {
            if (password == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Błędne dane logowania!");
            }
        }

        public void ThrowIfInvalidPassword(PasswordVerificationResult result)
        {
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Wprowadzono niepoprawne hasło!");
            }
        }
    }
}
