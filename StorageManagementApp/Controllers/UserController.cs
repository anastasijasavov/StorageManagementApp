using Microsoft.AspNetCore.Mvc;
using StorageManagementApp.Contracts.DTOs.Product;
using StorageManagementApp.Contracts.DTOs.User;
using StorageManagementApp.Contracts.Guards;
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
        [HttpGet]
        public ActionResult Login()
        {
            return View(new UserLoginDto());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginDto userDTO)
        {
            try
            {
                var result = _userService.Login(userDTO);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else {
                    userDTO.ErrorMessage = "Wrong email and/or password. Try again.";
                    return View(userDTO); //with error message
                }
            }
            catch
            {
                return View();
            }
        }

        [PrivateGuard]
        public ActionResult Index()
        {
            var productsDtos = _productService.GetProducts();
            if (productsDtos != null)
                return View(productsDtos);
            return View();
        }


    }
}
