using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp_Server.Models.Get;

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

        public GetProductDto Get(int id)
        {
            _numberValidator.ThrowIfIdIsNonPositive(id);

            var product = _ProductRepository.GetProduct(id);

            _productValidator.ThrowIfProductIsNull(product);

            var productDto = _mapper.Map<GetProductDto>(product);

            return productDto;
        }

        public ProductResultDto GetBadProductsResult(PaginationDto dto)
        {
            _paginatorValidator.ValidatePagination(dto);

            int counterProducts = _ProductRepository.CountBadProducts();

            var products = _ProductRepository.GetBadProducts(dto);

            var productsDto = _mapper.Map<List<GetProductDto>>(products);

            var productResultDto = new ProductResultDto()
            {
                Products = productsDto,
                TotalCount = counterProducts,
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

        public void RemoveProduct(int id)
        {
            _numberValidator.ThrowIfIdIsNonPositive(id);

            var product = _ProductRepository.GetProduct(id);

            _productValidator.ThrowIfProductIsNull(product);

            _ProductRepository.RemoveProduct(product!);
        }
    }
}