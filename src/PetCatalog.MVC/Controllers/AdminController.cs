using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Auth;
using PetCatalog.Domain.Models;
using PetCatalog.MVC.ViewModels;
using System.Collections.Generic;

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

            if (!Request.Cookies.TryGetValue("accessToken", out accessToken))
            {
                accessToken = HttpContext.Session.GetString("accessToken");
                
            }
            HttpContext.Session.Remove("accessToken");
            Response.Cookies.Delete("accessToken");
            if (Request.Cookies.TryGetValue("refreshToken", out refreshToken))
            {
                refreshToken = HttpContext.Session.GetString("refreshToken");
                
            }
            HttpContext.Session.Remove("refreshToken");
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
            if (id <= 0)
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
                if (id <= 0)
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
            if (id <= 0) return Redirect(url);
            animalService.DeleteComment(id);
            return Redirect(url);
        }

    }
}
