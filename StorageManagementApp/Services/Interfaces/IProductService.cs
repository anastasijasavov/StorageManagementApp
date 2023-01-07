using StorageManagementApp.Contracts.DTOs;
using StorageManagementApp.Contracts.DTOs.Product;

namespace StorageManagementApp.Mvc.Services.Interfaces
{
    public interface IProductService
    {
        ResponseTemplateDto<List<ProductViewDto>> GetProducts();
        Task<bool> AddProduct(ProductDto product);
        Task<bool> UpdateProduct(ProductDto product);
        Task<bool> DeleteProduct(int id);
        Task<ProductDto> GetProductById(int id);
        ResponseTemplateDto<List<ProductViewDto>> SearchProducts(ProductQuery query);
        Task<string> UploadFile(IFormFile file);
    }
}
