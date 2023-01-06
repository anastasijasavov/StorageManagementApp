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

        public ActionResult Index()
        {
            var productsDtos = _productService.GetProducts();
            if (productsDtos?.Data != null)
            {
                var productVm = new ProductVM
                {
                    Products = productsDtos.Data,
                    Query = new()
                };
                return View(productVm);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(ProductVM products)
        {
            return View(products);
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
            if (res) return RedirectToAction("Index", "Product");
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
                return RedirectToAction("Index", "Product");
            }
            else return View(dto);
        }

        [HttpGet]
        public ActionResult ShowConfirmDialog(int id)
        {
            return PartialView("ConfirmDialog", new IdDto { Id = id});
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult Search(ProductQuery query)
        {

            var products = this._productService.SearchProducts(query);

            ProductVM productVM = new() {
                Query = query,
                Products = products.Data
            };

            return View("Index", productVM);
        }
    }
}
