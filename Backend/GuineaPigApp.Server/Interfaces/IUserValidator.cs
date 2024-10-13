namespace GuineaPigApp.Server.Interfaces
{
    public interface IUserValidator
    {
        void ValidateEmailFormat(string email);
    }
}
