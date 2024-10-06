using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Interfaces
{
    public interface IPaginatorValidator
    {
        void ValidatePagination(PaginationDto dto);
    }
}
