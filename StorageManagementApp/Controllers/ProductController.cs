using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using StorageManagementApp.Contracts.DTOs;
using StorageManagementApp.Mvc.ViewModels;
using StorageManagementApp.Mvc.Services.Interfaces;
using StorageManagementApp.Contracts.DTOs.Product;
using Microsoft.Extensions.Caching.Memory;

namespace StorageManagementApp.Mvc.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private const string queryCacheString = "query";
        private IMemoryCache _cache;
        public ProductController(IProductService productService, IMemoryCache cache)
        {
            _productService = productService;
            _cache = cache;
        }
        public ActionResult Index()
        {
            var productsDtos = _productService.GetProducts();
            var pagedList = productsDtos.Data.AsQueryable().ToPagedList(1, 10);
            if (productsDtos?.Data != null)
            {
                var productVm = new ProductVM
                {
                    Products = pagedList,
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
        public async Task<ActionResult> Create(ProductDto product)
        {
            string? contentType = product.File?.ContentType;

            if (contentType != null && !IsAllowedContentType(contentType))
            {
                product.ErrorMessage = "This file type is not allowed.";
                return View(product);
            }
            var res = await _productService.AddProduct(product);

            //reload page to update the list
            if (res) return RedirectToAction("Index", "Product");
            else
            {
                product.ErrorMessage = "Error adding the product. Try again.";
                return View(product); //with error message
            }
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var productDto = await _productService.GetProductById(id);
            return View(productDto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductDto dto)
        {
            string? contentType = dto.File?.ContentType;

            if (contentType != null && !IsAllowedContentType(contentType))
            {
                dto.ErrorMessage = "This file type is not allowed.";
                return View(dto);
            }
            var result = await _productService.UpdateProduct(dto);
            if (result)
            {
                if (_cache.TryGetValue(queryCacheString, out ProductQuery query))
                {
                    return RedirectToAction("Index", this.GetRouteFromQuery(query));
                }
                return RedirectToAction("Index", "Product");
            }
            else return View(dto);
        }

        private RouteValueDictionary GetRouteFromQuery(ProductQuery query)
        {
            return new RouteValueDictionary
            {
                { "Query.Name", query.Name },
                { "Query.Code", query.Code },
                { "Query.CategoryId", query.CategoryId },
                { "page", 1 }
            };
        }
        private bool IsAllowedContentType(string contentType)
        {
            var isAllowedType = contentType != null && (contentType.Contains("jpeg") ||
                                contentType.Contains("jpg") ||
                                contentType.Contains("png"));
            return isAllowedType;
        }

        [HttpGet]
        public async Task<ActionResult> ShowConfirmDialog(int id)
        {
            var product = await _productService.GetProductById(id);
            return View("Delete", product);
        }
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            if (_cache.TryGetValue(queryCacheString, out ProductQuery query))
            {
                return RedirectToAction("Index", this.GetRouteFromQuery(query));
            }
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult Index(ProductQuery query, int page = 1)
        {

            var products = this._productService.SearchProducts(query);

            ProductVM productVM = new()
            {
                Query = query,
                Products = products.Data.AsQueryable().ToPagedList(page, query.PageSize)
            };
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromSeconds(600));

            _cache.Set(queryCacheString, query, cacheEntryOptions);
            return View(productVM);
        }
    }
}
