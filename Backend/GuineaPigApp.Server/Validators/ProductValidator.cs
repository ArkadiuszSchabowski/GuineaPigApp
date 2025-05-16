using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Validators
{
    public class ProductValidator : IProductValidator
    {
        public void ThrowIfProductExist(Product? product)
        {
            if (product != null)
            {
                throw new ConflictException("Podany produkt istnieje już w bazie danych!");
            }
        }

        public void ThrowIfProductIsNotCorrect(ProductDto productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                throw new BadRequestException("Nazwa produktu jest wymagana!");
            }
            if (productDto.Name.Length < 3)
            {
                throw new BadRequestException("Nazwa produktu nie może być krótsza niż 3 znaki!");
            }
            if (productDto.Name.Length > 25)
            {
                throw new BadRequestException("Nazwa produktu nie może być dłuższa niż 25 znaków!");
            }

            if (string.IsNullOrWhiteSpace(productDto.Description))
            {
                throw new BadRequestException("Opis produktu jest wymagany!");
            }
            if (productDto.Description.Length < 15)
            {
                throw new BadRequestException("Opis produktu nie może być krótszy niż 15 znaków!");
            }
            if (productDto.Description.Length > 1000)
            {
                throw new BadRequestException("Opis produktu nie może być dłuższy niż 1000 znaków!");
            }
        }

        public void ThrowIfProductIsNull(Product? product)
        {
            if (product == null)
            {
                throw new BadRequestException("Nie znaleziono produktu o podanym Id!");
            }
        }
    }
}