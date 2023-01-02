using AutoMapper;
using StorageManagementApp.Contracts.DTOs;
using StorageManagementApp.Contracts.DTOs.Product;
using StorageManagementApp.Models;
using StorageManagementApp.Mvc.Services.Interfaces;

namespace StorageManagementApp.Mvc.Services
{
    public class ProductService : IProductService
    {
        private readonly StorageDBContext _ctx;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        public ProductService(StorageDBContext ctx, IMapper mapper, ILogger<ProductService> logger)
        {
            _ctx = ctx;
            _mapper = mapper;
            _logger = logger;
        }
        public bool AddProduct(ProductCreateDto product)
        {

            Product dbProduct = _mapper.Map<Product>(product);
            _ctx.Products.Add(dbProduct);

            ////generate code
            //dbProduct.Code = dbProduct.Category + "-" + dbProduct.Id;
            //_ctx.Update(dbProduct);
            _ctx.SaveChanges();

            return true;

        }

        public bool DeleteProduct(int id)
        {
            var product = _ctx.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product != null)
            {
                _ctx.Products.Remove(product);
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public ResponseTemplateDto<List<ProductViewDto>> GetProducts()
        {
            var products = _ctx.Products.ToList();
            if (products?.Count > 0)
            {
                List<ProductViewDto> dtoProducts = _mapper.Map<List<ProductViewDto>>(products);
                return new ResponseTemplateDto<List<ProductViewDto>>
                {
                    Data = dtoProducts,
                    IsSuccess = true,
                };
            }
            else
            {
                return new ResponseTemplateDto<List<ProductViewDto>>
                {
                    Data = null,
                    IsSuccess = false,
                    ErrorMessage = "No data!"
                };
            }

        }

        public ResponseTemplateDto<List<ProductViewDto>> SearchProducts(ProductQuery query)
        {
            var products = _ctx.Products.Where(x =>
                query.Name == null || x.Name == query.Name &&
                query.Code == null || x.Code == query.Code &&
                query.CategoryId == null || x.CategoryId == query.CategoryId
                );
            List<ProductViewDto> productDtos = _mapper.Map<List<ProductViewDto>>(products);
            return new ResponseTemplateDto<List<ProductViewDto>>
            {
                Data = productDtos,
                IsSuccess = true
            };

        }

        public bool UpdateProduct(ProductCreateDto product)
        {

            var dbProduct = _mapper.Map<Product>(product);
            _ctx.Products.Update(dbProduct);
            _ctx.SaveChanges();
            return true;

        }
    }
}
