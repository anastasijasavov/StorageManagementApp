using AutoMapper;
using StorageManagementApp.Contracts.DTOs.Product;
using StorageManagementApp.Models;
using StorageManagementApp.Mvc.Services.Interfaces;

namespace StorageManagementApp.Mvc.Services
{
    public class ProductService : IProductService
    {
        private readonly StorageDBContext _ctx;
        private readonly IMapper _mapper;
        public ProductService(StorageDBContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public bool AddProduct(ProductCreateDto product)
        {
            try
            {
                Product dbProduct = _mapper.Map<Product>(product);
                _ctx.Products.Add(dbProduct);

                ////generate code
                //dbProduct.Code = dbProduct.Category + "-" + dbProduct.Id;
                //_ctx.Update(dbProduct);
                _ctx.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
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
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public List<ProductViewDto> GetProducts()
        {
            try
            {
                var products = _ctx.Products.ToList();
                if (products != null)
                {
                    List<ProductViewDto> dtoProducts = _mapper.Map<List<ProductViewDto>>(products);
                    return dtoProducts;
                }
                return null;
            }
            catch (Exception)
            {
                return null; // with message
                throw;
            }
        }

        public bool UpdateProduct(ProductCreateDto product)
        {
            try
            {
                var dbProduct = _mapper.Map<Product>(product);
                _ctx.Products.Update(dbProduct);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
