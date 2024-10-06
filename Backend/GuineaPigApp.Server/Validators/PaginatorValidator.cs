using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Validators
{
    public class PaginatorValidator : IPaginatorValidator
    {
        public void ValidatePagination(PaginationDto dto)
        {
            if(dto.PageNumber < 1)
            {
                throw new BadRequestException("Podaj prawidłowy numer strony większy od zera!");
            }
            if(dto.PageSize < 1 || dto.PageSize > 100)
            {
                throw new BadRequestException("Ilość wyników musi mieścić się w przedziale 1-100");
            }
            if(dto.PageSize % 5 != 0)
            {
                throw new BadRequestException("Ilość wyników musi być podzielna przez 5");
            }
        }
    }
}
