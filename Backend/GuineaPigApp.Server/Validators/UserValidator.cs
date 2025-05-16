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
        public void ThrowIfUserExist(User? user)
        {
            if (user != null)
            {
                throw new ConflictException("Taki użytkownik istnieje już w bazie danych!");
            }
        }
        public void ThrowIfRemovingDefaultAccount(string email)
        {
            var defaultUserEmail = "user@gmail.com";
            var defaultManagerEmail = "manager@gmail.com";
            var defaultAdminEmail = "admin@gmail.com";

            if (email == defaultUserEmail || email == defaultManagerEmail || email == defaultAdminEmail)
            {
                throw new ForbiddenException("Nie możesz usunąć domyślnego konta użytkownika!");
            }
        }

        public void ThrowIfUserRoleIsNull(Role? role)
        {
            if (role == null)
            {
                throw new Exception("Użytkownik nie ma przypisanej roli!");
            }
        }
    }
}
