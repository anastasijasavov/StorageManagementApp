using Microsoft.AspNetCore.Mvc;

namespace StorageManagementApp.Mvc.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
