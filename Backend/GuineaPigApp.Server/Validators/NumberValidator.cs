using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;

namespace GuineaPigApp.Server.Validators
{
    public class NumberValidator : INumberValidator
    {
        public void ThrowIfIdIsNonPositive(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wartość Id musi być większa od 0!");
            }
        }
    }
}
