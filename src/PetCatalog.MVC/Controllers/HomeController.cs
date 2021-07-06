using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCatalog.Application.Interfaces;
using PetCatalog.MVC.Extensions;
using PetCatalog.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Controllers
{   
    public class HomeController : Controller
    {
        private readonly IAnimalService animalService;
        private readonly IMapper mapper;

        public HomeController(IAnimalService animalService,IMapper mapper)
        {
            this.animalService = animalService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var bestAnimals = mapper.Map<IEnumerable<AnimalViewModel>>(animalService.GetBestAnimals()); 
            return View(bestAnimals);
        }
    }
}
