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
        private ICategoryService animalService;
        public CatalogController(ICategoryService animalService)
        {
            this.animalService = animalService;
        }
        public IActionResult Index()
        {
            
            var model = animalService.GetCategorys();
            return View(model);
        }
        public IActionResult Animal(int? id)
        {
            if (id is null) return RedirectToAction("index");

            return View();
        }
    }
}
