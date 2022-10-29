using Microsoft.AspNetCore.Mvc;
using StorageManagementApp.Contracts.DTOs.Product;
using StorageManagementApp.Mvc.Services.Interfaces;

namespace StorageManagementApp.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public IActionResult Create(ProductCreateDto product)
        {
            var res = _productService.AddProduct(product);

            if (res) return View(); //with toast success
            else return View(); //with error message
        }

    }
}
