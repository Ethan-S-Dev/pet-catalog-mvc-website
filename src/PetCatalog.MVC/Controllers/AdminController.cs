using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public AdminController(ICategoryService categoryService, IAnimalService animalService, ICommentService commentService,IMapper mapper)
        {
            this.commentService = commentService;
            this.categoryService = categoryService;
            this.animalService = animalService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = categoryService.GetCategorys();
            return View(model);
        }

        public IActionResult NewAnimal()
        {
            var animalFm = mapper.Map<AnimalFormModel>(new AnimalViewModel());
            animalFm.Categorys = mapper.Map<IEnumerable<CategoryViewModel>>(categoryService.GetCategorys());
            return View(animalFm);
        }

        [HttpGet]
        public IActionResult EditAnimal(int id)
        {
            var animal = animalService.GetAnimal(id);
            if(animal is null) return RedirectToAction("Index");
            var animalEm = mapper.Map<AnimalEditModel>(animal);
            animalEm.Categorys = mapper.Map<IEnumerable<CategoryViewModel>>(categoryService.GetCategorys());
            return View(animalEm);
        }

        public IActionResult DeleteAnimal(int id)
        {
            var animal = animalService.GetAnimal(id);
            if(animal is null) return RedirectToAction("Index");

            animalService.DeleteAnimal(id);
            commentService.DeleteComments(id);                  

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAnimal(AnimalFormModel animalForm)
        {
            if (ModelState.IsValid)
            {
                var animalVm = mapper.Map<AnimalViewModel>(animalForm);
                animalVm.SetCategory(animalForm,categoryService);
                var animal = mapper.Map<Animal>(animalVm);
                animalService.AddAnimal(animal);

                return RedirectToAction("Index");
            }

            return View("NewAnimal", animalForm);
        }

        [HttpPost]
        public IActionResult EditAnimal(AnimalEditModel animalForm)
        {

            if (ModelState.IsValid)
            {
                var animalVm = mapper.Map<AnimalViewModel>(animalForm);
                animalVm.SetCategory(animalForm, categoryService);
                var animal = mapper.Map<Animal>(animalVm);
                animalService.EditAnimal(animal);
                return RedirectToAction("Index");
            }

            animalForm.Categorys = mapper.Map<IEnumerable<CategoryViewModel>>(categoryService.GetCategorys());
            IActionResult result = View("EditAnimal", animalForm);
            return result;
        }
    }
}
