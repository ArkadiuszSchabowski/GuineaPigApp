using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;

namespace GuineaPigApp.Server.Validators
{
    public class UserValidator : IUserValidator
    {
        public void ThrowIfUserIsNull(User? user)
        {
            if (user == null)
            {
                throw new NotFoundException("Nie znaleziono takiego użytkownika!");
            }
        }
    }
}
