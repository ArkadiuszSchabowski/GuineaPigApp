using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class ProductService : IProductService, IAddService<ProductDto>, IGetService<GetProductDto>
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

        public void Add(ProductDto dto)
        {
            var product = _repository.EnsureProductDoesNotExist(dto.Name);

            if (product != null)
            {
                throw new ConflictException("Podany produkt istnieje już w bazie danych!");
            }

            var newProduct = _mapper.Map<Product>(dto);

            _repository.AddProduct(newProduct);
        }

        public ProductResultDto GetBadProductsResult(PaginationDto dto)
        {
            _paginator.ValidatePagination(dto);

            int countBadProducts = _repository.CountBadProducts();

            var badProducts = _repository.GetBadProducts(dto);

            var badProductsDto = _mapper.Map<List<ProductDto>>(badProducts);

            var productResultDto = new ProductResultDto()
            {
                Products = badProductsDto,
                TotalCount = countBadProducts,
            };

            return productResultDto;
        }

        public ProductResultDto GetGoodProductsResult(PaginationDto dto)
        {
            _paginator.ValidatePagination(dto);

            int countGoodProducts = _repository.CountGoodProducts();

            var goodProducts = _repository.GetGoodProducts(dto);

            var goodProductsDto = _mapper.Map<List<ProductDto>>(goodProducts);

            var productResultDto = new ProductResultDto()
            {
                Products = goodProductsDto,
                TotalCount = countGoodProducts,
            };
            return productResultDto;
        }

        public GetProductDto Get(int id)
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

            var productDto = _mapper.Map<GetProductDto>(product);

            return productDto;
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