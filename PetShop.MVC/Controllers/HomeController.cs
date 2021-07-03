using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnimalService animalService;
        public HomeController(IAnimalService animalService)
        {
            this.animalService = animalService;
        }

        //[RequestFormLimits(MultipartBodyLengthLimit = 268435456)]
        public IActionResult Index()
        {
            var bestAnimals = animalService.GetBestAnimals();
            return View(bestAnimals);
        }
    }
}
