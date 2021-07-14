using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using PetCatalog.Application.Auth;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.MVC.Extensions;
using PetCatalog.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Controllers
{

    public class AdminController : Controller
    {

        private readonly ICategoryService categoryService;
        private readonly IAnimalService animalService;
        private readonly IAuthService authService;

        private readonly IMapper mapper;
        private readonly int defaultId;
        public AdminController(ICategoryService categoryService,
                                IAuthService authService,
                                IConfiguration configuration,
                                IAnimalService animalService,
                                IMapper mapper)
        {
            this.categoryService = categoryService;
            this.animalService = animalService;
            this.authService = authService;
            defaultId = configuration.GetValue<int>("DefaultImageId");
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Index()
        {
            var model = mapper.Map<IEnumerable<CategoryViewModel>>(categoryService.GetCategorys());
            return View(model);

            //return RedirectToAction("Get", "Login");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Logout(bool logoutAll)
        {
            string accessToken;
            string refreshToken;

            Request.Cookies.TryGetValue("accessToken", out accessToken);
            Request.Cookies.TryGetValue("refreshToken", out refreshToken);

            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");

            var request = new RefreshRequest() { AccessToken = accessToken, RefreshToken = refreshToken };

            if (logoutAll)
                authService.DeleteAllRefreshToken(request);
            else
                authService.DeleteRefreshToken(request);

            return RedirectToAction("Index", "Login");
        }

        [Authorize]
        [HttpGet]
        public IActionResult AnimalForm(int id)
        {
            ViewBag.DefaultImageId = defaultId;
            Animal animal;
            if (id == 0)
                animal = animalService.GetEmptyAnimal();
            else
                animal = animalService.GetAnimal(id);

            if (animal is null) return RedirectToAction("Index");
            var animaVm = mapper.Map<AnimalViewModel>(animal);
            return View(animaVm);
        }

        [Authorize]
        public IActionResult DeleteAnimal(int id)
        {

            var animal = animalService.GetAnimal(id);
            if (animal is null) return RedirectToAction("Index");

            animalService.DeleteAnimal(id);

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AnimalForm(AnimalViewModel animalVm, int id)
        {
            if (ModelState.IsValid)
            {
                animalVm.AnimalId = id;
                var animal = mapper.Map<Animal>(animalVm);
                if (id == 0)
                {
                    animalService.AddAnimal(animal);
                }
                else
                {
                    animalService.EditAnimal(animal);
                }
                return RedirectToAction("Index");
            }

            return View("AnimalForm", animalVm);
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteComment(int id)
        {
            var url = Request.Headers["Referer"].ToString();
            if (id == 0) return Redirect(url);
            animalService.DeleteComment(id);
            return Redirect(url);
        }

    }
}
