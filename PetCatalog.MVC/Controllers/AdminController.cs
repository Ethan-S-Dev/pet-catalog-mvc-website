using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCatalog.Application.Interfaces;
using PetCatalog.Application.ViewModels;
using PetCatalog.MVC.Extensions;
using PetCatalog.MVC.Models;
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
        private readonly IImageService imageService;
        private readonly IAnimalService animalService;
        private readonly IMapper mapper;
        public AdminController(ICategoryService categoryService, IAnimalService animalService, ICommentService commentService,IImageService imageService,IMapper mapper)
        {
            this.imageService = imageService;
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
            animalFm.Categorys = categoryService.GetCategorys();
            return View(animalFm);
        }

        [HttpGet]
        public IActionResult EditAnimal(int id)
        {
            var animal = animalService.GetAnimal(id);
            if(animal is null) return RedirectToAction("Index");
            var animalEm = mapper.Map<AnimalEditModel>(animal);
            animalEm.Categorys = categoryService.GetCategorys();
            return View(animalEm);
        }

        public IActionResult DeleteAnimal(int id)
        {
            var animal = animalService.GetAnimal(id);
            if(animal is null) return RedirectToAction("Index");

            animalService.DeleteAnimal(id);
            commentService.DeleteComments(id);
            imageService.DeleteImage(animal.PictureName);                    

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAnimal(AnimalFormModel animalForm)
        {
            if (ModelState.IsValid)
            {
                var animalVm = mapper.Map<AnimalViewModel>(animalForm);

                animalVm.SetCategory(animalForm,categoryService);
                using var imgStream = animalVm.SetPicture(animalForm);
                animalService.AddAnimal(animalVm);
                if (!imageService.SaveImage(animalVm.PictureName, imgStream))
                {
                    animalVm.PictureName = imageService.DefaultName;
                    animalService.EditAnimal(animalVm);
                }

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
                animalVm.SetPicture(animalForm,imageService);
                animalService.EditAnimal(animalVm);
                return RedirectToAction("Index");
            }

            animalForm.Categorys = categoryService.GetCategorys();
            IActionResult result = View("EditAnimal", animalForm);
            return result;
        }
    }
}
