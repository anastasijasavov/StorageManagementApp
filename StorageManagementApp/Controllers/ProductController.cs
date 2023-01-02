using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorageManagementApp.Contracts.DTOs;
using StorageManagementApp.Contracts.DTOs.Product;
using StorageManagementApp.Mvc.Services.Interfaces;

namespace StorageManagementApp.Mvc.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductDto());
        }

        [HttpPost]
        public ActionResult Create(ProductDto product)
        {
            var res = _productService.AddProduct(product);

            //reload page to update the list
            if (res) return RedirectToAction("Index", "User");
            else
            {
                product.ErrorMessage = "Error adding the product. Try again.";
                return View(product); //with error message
            }
        }

        public ActionResult Edit(int id)
        {
            var productDto = _productService.GetProductById(id);
            return View(productDto);
        }

        [HttpPost]
        public ActionResult Edit(ProductDto dto)
        {

            var result = _productService.UpdateProduct(dto);
            if (result)
            {
                return RedirectToAction("Index", "User");
            }
            else return View(dto);
        }

        [HttpGet]
        public ActionResult ShowConfirmDialog(int id)
        {
            return PartialView("ConfirmDialog", new IdDto { Id = id});
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index", "User");
        }
    }
}
