using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IGetService<T> where T : class
    {
        T Get(int id);
    }
}