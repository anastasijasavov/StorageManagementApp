using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorageManagementApp.Contracts.DTOs;
using StorageManagementApp.Contracts.DTOs.Product;
using StorageManagementApp.Contracts.ExceptionHandler;
using StorageManagementApp.Models;
using StorageManagementApp.Mvc.Services.Interfaces;

namespace StorageManagementApp.Mvc.Services
{
    public class ProductService : IProductService
    {
        private readonly StorageDBContext _ctx;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironemnt;
        public ProductService(StorageDBContext ctx, IMapper mapper, ILogger<ProductService> logger, IWebHostEnvironment webHostEnvironemnt)
        {
            _ctx = ctx;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironemnt = webHostEnvironemnt;
        }
        public async Task<bool> AddProduct(ProductDto product)
        {

            Product dbProduct = _mapper.Map<Product>(product);

            if (product.File != null)
            {
                dbProduct.ImagePath = await UploadFile(product.File);
            }
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

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _ctx.Products.Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public ResponseTemplateDto<List<ProductViewDto>> GetProducts()
        {
            var products = _ctx.Products.Include(x => x.Category).AsNoTracking().ToList();
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
                (query.Code == null && (
                    (query.Name == null || x.Name.Contains(query.Name)) &&
                    (query.CategoryId == null ||
                     query.CategoryId == 0 ||
                     x.CategoryId == query.CategoryId))
                || x.Code == query.Code)
                ).Include(x => x.Category);

            List<ProductViewDto> productDtos = _mapper.Map<List<ProductViewDto>>(products);
            return new ResponseTemplateDto<List<ProductViewDto>>
            {
                Data = productDtos,
                IsSuccess = true
            };

        }

        public async Task<bool> UpdateProduct(ProductDto product)
        {

            var dbProduct = _ctx.Products.FirstOrDefault(x => x.Id == product.Id);
            if (dbProduct != null)
            {
                var updateProduct = _mapper.Map<Product>(product);
                _ctx.Entry(dbProduct).CurrentValues.SetValues(updateProduct);
                if(product.File != null)
                {
                    dbProduct.ImagePath = await UploadFile(product.File);
                }

                await _ctx.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> UploadFile(IFormFile? file)
        {
            if (file == null) return "";

            var wwwRoot = _webHostEnvironemnt.WebRootPath;
            var uploadsDirectoryName = "images";
            var uploadsDirectory = Path.Combine(wwwRoot, uploadsDirectoryName);

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var uniqueName = Guid.NewGuid().ToString();
            var newFile = uniqueName + Path.GetExtension(file.FileName);
            var fileUploadPath = Path.Combine(uploadsDirectory, newFile);

            using FileStream fs = new FileStream(fileUploadPath, FileMode.Create, FileAccess.Write);
            await file.CopyToAsync(fs);
            return Path.Combine(uploadsDirectoryName, newFile);
        }
    }
}
