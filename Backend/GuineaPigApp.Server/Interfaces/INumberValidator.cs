namespace GuineaPigApp.Server.Interfaces
{
    public interface INumberValidator
    {
        void ThrowIfIdIsNonPositive(int id);
    }
}
