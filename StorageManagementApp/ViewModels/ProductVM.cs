using PagedList.Core;
using StorageManagementApp.Contracts.DTOs.Product;

namespace StorageManagementApp.Mvc.ViewModels
{
    public class ProductVM
    {
        public IPagedList<ProductViewDto> Products { get; set; }
        public ProductQuery Query { get; set; }
    }
}
