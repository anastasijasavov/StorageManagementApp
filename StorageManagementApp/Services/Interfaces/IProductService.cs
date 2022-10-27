using StorageManagementApp.Contracts.DTOs.Product;

namespace StorageManagementApp.Mvc.Services.Interfaces
{
    public interface IProductService
    {
        bool AddProduct(ProductCreateDto product);
        bool UpdateProduct(ProductCreateDto product);
        bool DeleteProduct(int id);
    }
}
