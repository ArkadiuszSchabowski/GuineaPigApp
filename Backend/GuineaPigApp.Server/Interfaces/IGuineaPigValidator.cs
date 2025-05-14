using GuineaPigApp.Server.Database.Entities;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IGuineaPigValidator
    {
        void ThrowIfGuineaPigIsNull(GuineaPig? guineaPig);
        void ThrowIfUserGuineaPigExists(bool guineaPig);
        void ThrowIfWeightIsNotCorrect(int weight);
    }
}
