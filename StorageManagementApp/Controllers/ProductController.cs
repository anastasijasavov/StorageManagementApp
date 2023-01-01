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

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductCreateDto());
        }

        [HttpPost]
        public IActionResult Create(ProductCreateDto product)
        {
            var res = _productService.AddProduct(product);

            //reload page to update the list
            if (res) return RedirectToAction("Index","User");
            else
            {
                product.ErrorMessage = "Error adding the product. Try again.";
                return View(product); //with error message
            } 
        }

    }
}
