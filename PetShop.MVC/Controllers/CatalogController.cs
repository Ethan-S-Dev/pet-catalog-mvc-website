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
        private ICatalogService animalService;
        public CatalogController(ICatalogService animalService)
        {
            this.animalService = animalService;
        }
        public IActionResult Index()
        {
            
            var model = animalService.GetCatagorys();
            return View(model);
        }
        public IActionResult Animal(int? id)
        {
            if (id is null) return RedirectToAction("index");

            return View();
        }
    }
}
