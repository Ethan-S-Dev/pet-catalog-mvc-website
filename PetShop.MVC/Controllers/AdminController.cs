using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.MVC.Controllers
{
    public class AdminController : Controller
    {
        private IWebHostEnvironment webHostEnvironment;
        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {           
            return View();
        }

        [HttpPost]
        public IActionResult ImageUplaod(IFormFile photo)
        {
            var unsafeFileName = Path.GetFileName(photo.FileName);
            var exten = Path.GetExtension(unsafeFileName);
            var newFileName = $"{Guid.NewGuid()}{exten}";
            var path = Path.Combine(webHostEnvironment.WebRootPath, "res/images/animals", newFileName);
            using (var stream = new FileStream(path, FileMode.Create))
                photo.CopyToAsync(stream);
            return View();
        }
    }
}
