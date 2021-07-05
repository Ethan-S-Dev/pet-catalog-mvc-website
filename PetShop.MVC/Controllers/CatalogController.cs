using Microsoft.AspNetCore.Mvc;
using PetCatalog.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Controllers
{    
    public class CatalogController : Controller
    {
        private ICategoryService categoryService;
        public CatalogController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {           
            var model = categoryService.GetCategorys();
            return View(model);
        }
        public IActionResult Animal(int id)
        {
            if (id == 0) return RedirectToAction("index");

            return View();
        }
    }
}
