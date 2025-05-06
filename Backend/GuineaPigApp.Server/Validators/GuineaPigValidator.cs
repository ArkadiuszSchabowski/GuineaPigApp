using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;

namespace GuineaPigApp.Server.Validators
{
    public class GuineaPigValidator : IGuineaPigValidator
    {
        public void ThrowIfGuineaPigIsNull(GuineaPig? guineaPig)
        {
            if (guineaPig == null)
            {
                throw new BadRequestException("Nie posiadasz świnki morskiej o takim imieniu!");
            }
        }

        public void ThrowIfWeightIsNotCorrect(int weight)
        {
            if (weight < 50 || weight > 3000)
            {
                throw new BadRequestException("Waga świnki musi mieścić się w przedziale 50 do 3000gram!");
            }
        }
    }
}
