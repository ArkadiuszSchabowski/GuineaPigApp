using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Services
{
    public class ProductService : IProductService, IAddService<ProductDto>, IGetService<GetProductDto>
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IProductValidator _productValidator;
        private readonly IPaginatorValidator _paginatorValidator;
        private readonly INumberValidator _numberValidator;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IProductValidator productValidator, IPaginatorValidator paginatorValidator, INumberValidator numberValidator, IMapper mapper)
        {
            _ProductRepository = productRepository;
            _productValidator = productValidator;
            _paginatorValidator = paginatorValidator;
            _numberValidator = numberValidator;
            _mapper = mapper;
        }

        public void Add(ProductDto dto)
        {
            var product = _ProductRepository.GetProduct(dto.Name);

            _productValidator.ThrowIfProductExist(product);

            var newProduct = _mapper.Map<Product>(dto);

            _ProductRepository.AddProduct(newProduct);
        }

        public ProductResultDto GetBadProductsResult(PaginationDto dto)
        {
            _paginatorValidator.ValidatePagination(dto);

            int countBadProducts = _ProductRepository.CountBadProducts();

            var badProducts = _ProductRepository.GetBadProducts(dto);

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

            int countGoodProducts = _ProductRepository.CountGoodProducts();

            var goodProducts = _ProductRepository.GetGoodProducts(dto);

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

            var product = _ProductRepository.GetProduct(id);

            _productValidator.ThrowIfProductIsNull(product);

            var productDto = _mapper.Map<GetProductDto>(product);

            return productDto;
        }

        public void RemoveProduct(int id)
        {
            _numberValidator.ThrowIfIdIsNonPositive(id);

            var product = _ProductRepository.GetProduct(id);

            _productValidator.ThrowIfProductIsNull(product);

            _ProductRepository.RemoveProduct(product!);
        }
    }
}