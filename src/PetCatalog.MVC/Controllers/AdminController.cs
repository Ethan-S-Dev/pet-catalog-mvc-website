using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        private readonly ICommentService commentService;
        private readonly ICategoryService categoryService;
        private readonly IAnimalService animalService;

        private readonly IMapper mapper;
        private readonly int defaultId;
        public AdminController(ICategoryService categoryService, IConfiguration configuration, IAnimalService animalService, ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService;
            this.categoryService = categoryService;
            this.animalService = animalService;
            defaultId = configuration.GetValue<int>("DefaultImageId");
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = mapper.Map<IEnumerable<CategoryViewModel>>(categoryService.GetCategorys());
            return View(model);
        }


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

        public IActionResult DeleteAnimal(int id)
        {
            var animal = animalService.GetAnimal(id);
            if (animal is null) return RedirectToAction("Index");

            animalService.DeleteAnimal(id);
            commentService.DeleteComments(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AnimalForm(AnimalViewModel animalVm, int id)
        {
            if (ModelState.IsValid)
            {
                animalVm.SetCategory(categoryService);
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

    }
}
