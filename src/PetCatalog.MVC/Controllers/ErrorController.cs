using Microsoft.AspNetCore.Mvc;

namespace PetCatalog.MVC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
