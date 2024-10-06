using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPaginatorValidator _paginator;

        public ProductService(IProductRepository repository, IMapper mapper, IPaginatorValidator paginator)
        {
            _repository = repository;
            _mapper = mapper;
            _paginator = paginator;
        }

        public void AddProduct(ProductDto dto)
        {
            var product = _repository.EnsureProductDoesNotExist(dto.Name);

            if (product != null)
            {
                throw new ConflictException("Podany produkt istnieje już w bazie danych!");
            }

            var newProduct = _mapper.Map<Product>(dto);

            _repository.AddProduct(newProduct);
        }

        public List<Product> GetBadProducts(PaginationDto dto)
        {
            _paginator.ValidatePagination(dto);

            var badProducts = _repository.GetBadProducts(dto);

            return badProducts;
        }

        public List<Product> GetGoodProducts(PaginationDto dto)
        {
            _paginator.ValidatePagination(dto);

            var goodProducts = _repository.GetGoodProducts(dto);

            return goodProducts;
        }

        public Product GetProduct(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wartość Id musi być większa od 0!");
            }

            var product = _repository.GetProduct(id);

            if (product == null)
            {
                throw new BadRequestException("Nie znaleziono produktu o podanym Id!");
            }

            return product;
        }

        public void RemoveProduct(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wartość Id musi być większa od 0!");
            }

            var product = _repository.GetProduct(id);

            if (product == null)
            {
                throw new BadRequestException("Nie znaleziono produktu o podanym Id!");
            }

            _repository.RemoveProduct(product);
        }
    }
}