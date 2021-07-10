using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCatalog.Application.Auth;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Models;
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
        private readonly IAuthService authService;

        public HomeController(IAnimalService animalService, IMapper mapper, IAuthService authService)
        {
            this.animalService = animalService;
            this.mapper = mapper;
            this.authService = authService;
        }

        public IActionResult Index()
        {
            var animals = animalService.GetBestAnimals();
            var bestAnimals = mapper.Map<IEnumerable<AnimalViewModel>>(animals);
            return View(bestAnimals);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var user = authService.GetEmptyUser();
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var userWithToken = authService.Authenticate(user);

            if (userWithToken is null)
                return RedirectToAction("Login");

            HttpContext.Session.SetString("access-token", userWithToken.AccessToken);

            return View("Login", userWithToken);
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken(RefreshRequest refreshRequest)
        {
            var userWithToken = authService.RefreshToken(refreshRequest);

            if (userWithToken is null)
                return RedirectToAction("Login");

            HttpContext.Session.Remove("access-token");
            HttpContext.Session.SetString("access-token", userWithToken.AccessToken);

            return null;
        }
    }
}
