using StorageManagementApp.Contracts.DTOs;
using StorageManagementApp.Contracts.DTOs.Product;

namespace StorageManagementApp.Mvc.Services.Interfaces
{
    public interface IProductService
    {
        ResponseTemplateDto<List<ProductViewDto>> GetProducts();
        bool AddProduct(ProductDto product);
        bool UpdateProduct(ProductDto product);
        bool DeleteProduct(int id);
        ProductDto GetProductById(int id);
        ResponseTemplateDto<List<ProductViewDto>> SearchProducts(ProductQuery query);
    }
}
