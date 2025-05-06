using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class ProductService : IProductService, IAddService<ProductDto>, IGetService<GetProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPaginatorValidator _paginatorValidator;
        private readonly IProductValidator _productValidator;
        private readonly INumberValidator _numberValidator;

        public ProductService(IProductRepository repository, IMapper mapper, IProductValidator productValidator, IPaginatorValidator paginatorValidator, INumberValidator numberValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _paginatorValidator = paginatorValidator;
            _productValidator = productValidator;
            _numberValidator = numberValidator;
        }

        public void Add(ProductDto dto)
        {
            var product = _repository.GetProduct(dto.Name);

            _productValidator.ThrowIfProductExist(product);

            var newProduct = _mapper.Map<Product>(dto);

            _repository.AddProduct(newProduct);
        }

        public ProductResultDto GetBadProductsResult(PaginationDto dto)
        {
            _paginatorValidator.ValidatePagination(dto);

            int countBadProducts = _repository.CountBadProducts();

            var badProducts = _repository.GetBadProducts(dto);

            var badProductsDto = _mapper.Map<List<GetProductDto>>(badProducts);

            var productResultDto = new ProductResultDto()
            {
                Products = badProductsDto,
                TotalCount = countBadProducts,
            };

            return productResultDto;
        }

        public ProductResultDto GetGoodProductsResult(PaginationDto dto)
        {
            _paginatorValidator.ValidatePagination(dto);

            int countGoodProducts = _repository.CountGoodProducts();

            var goodProducts = _repository.GetGoodProducts(dto);

            var goodProductsDto = _mapper.Map<List<GetProductDto>>(goodProducts);

            var productResultDto = new ProductResultDto()
            {
                Products = goodProductsDto,
                TotalCount = countGoodProducts,
            };
            return productResultDto;
        }

        public GetProductDto Get(int id)
        {
            _numberValidator.ThrowIfIdIsNonPositive(id);

            var product = _repository.GetProduct(id);

            _productValidator.ThrowIfProductIsNull(product);

            var productDto = _mapper.Map<GetProductDto>(product);

            return productDto;
        }

        public void RemoveProduct(int id)
        {
            _numberValidator.ThrowIfIdIsNonPositive(id);

            var product = _repository.GetProduct(id);

            _productValidator.ThrowIfProductIsNull(product);

            _repository.RemoveProduct(product!);
        }
    }
}