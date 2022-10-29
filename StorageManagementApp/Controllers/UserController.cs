using Microsoft.AspNetCore.Mvc;
using StorageManagementApp.Contracts.DTOs.Product;
using StorageManagementApp.Contracts.DTOs.User;
using StorageManagementApp.Mvc.Services.Interfaces;

namespace StorageManagementApp.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public UserController(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateDto user)
        {
            _userService.CreateUser(user);
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginDto userDTO)
        {
            try
            {
                var result = _userService.Login(userDTO);
                if (result)
                    return RedirectToAction(nameof(Index));
                else return View(); //with error message
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {
            var productsDtos = _productService.GetProducts();
            return View(productsDtos);
        }

      
    }
}
