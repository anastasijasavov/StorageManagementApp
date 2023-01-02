using StorageManagementApp.Contracts.DTOs;
using StorageManagementApp.Contracts.DTOs.Product;

namespace StorageManagementApp.Mvc.Services.Interfaces
{
    public interface IProductService
    {
        ResponseTemplateDto<List<ProductViewDto>> GetProducts();
        bool AddProduct(ProductCreateDto product);
        bool UpdateProduct(ProductCreateDto product);
        bool DeleteProduct(int id);
        ResponseTemplateDto<List<ProductViewDto>> SearchProducts(ProductQuery query);
    }
}
