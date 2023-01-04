﻿using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserCreateDto user)
        {
            var res = await _userService.CreateUser(user);
            if (res)
            {
                return View("Index");
            }else
            {
                user.ErrorMessage = "Registration failed.";
                return View(user);
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View(new UserLoginDto());
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            var res = await _userService.Logout();
            if (res)
            {
                return RedirectToAction("Index", "Home");
            }
            else return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginDto userDTO)
        {
            try
            {
                var result = await _userService.Login(userDTO);
                if (result)
                {
                    var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
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
        [Authorize]
        public ActionResult Index()
        {
            var productsDtos = _productService.GetProducts();
            if (productsDtos != null)
                return View(productsDtos.Data);
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(List<ProductViewDto> products)
        {
            return View(products);
        }
    }
}
