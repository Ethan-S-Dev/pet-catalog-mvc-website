using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.MVC.Controllers
{
    public class CatalogController : Controller
    {
        private ICategoryService catalogService;
        public CatalogController(ICategoryService catalogService)
        {
            this.catalogService = catalogService;
        }
        public IActionResult Index()
        {           
            var model = catalogService.GetCategorys();
            return View(model);
        }
        public IActionResult Animal(int? id)
        {
            if (id is null) return RedirectToAction("index");

            return View();
        }
    }
}
