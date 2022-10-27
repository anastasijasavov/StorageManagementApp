using Microsoft.AspNetCore.Mvc;
using StorageManagementApp.Models;
using System.Diagnostics;

namespace StorageManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StorageDBContext _storageDBContext;
        public HomeController(StorageDBContext ctx, ILogger<HomeController> logger)
        {
            _storageDBContext = ctx;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //}
    }
}