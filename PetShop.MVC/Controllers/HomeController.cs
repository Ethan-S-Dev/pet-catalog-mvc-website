using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        [RequestFormLimits(MultipartBodyLengthLimit = 268435456)]
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
