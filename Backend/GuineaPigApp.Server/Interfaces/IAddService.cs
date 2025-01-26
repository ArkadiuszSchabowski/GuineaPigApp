namespace GuineaPigApp.Server.Interfaces
{
    public interface IAddService<T> where T : class
    {
        void Add(T item);
    }
}
