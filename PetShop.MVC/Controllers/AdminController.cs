using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICategoryService categoryService;
        private readonly IAnimalService animalService;
        public AdminController(IWebHostEnvironment webHostEnvironment,ICategoryService categoryService,IAnimalService animalService)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.categoryService = categoryService;
            this.animalService = animalService;
        }
        public IActionResult Index()
        {           
            return View();
        }

        [Route("{controller}/create")]
        public IActionResult NewAnimal()
        {
            return View(new AnimalFormModel(categoryService));
        }

        [HttpPost]
        public IActionResult AddAnimal(AnimalFormModel animalForm)
        {
            if(ModelState.IsValid)
            {
                if(animalForm.CategoryId == -1)
                {
                    if (!categoryService.AddCategory(animalForm.CategoryName,out int id))
                        return View("NewAnimal");

                    animalForm.Animal.CategoryId = id;

                }
                animalForm.Animal.CategoryId = animalForm.CategoryId;
                
                var unsafeFileName = Path.GetFileName(animalForm.Image.FileName);
                var exten = Path.GetExtension(unsafeFileName);
                var newFileName = $"{Guid.NewGuid()}{exten}";
                var path = Path.Combine(webHostEnvironment.WebRootPath, "res/images/animals", newFileName);
                try
                {                  
                    
                    using (var stream = new FileStream(path, FileMode.Create))
                        animalForm.Image.CopyToAsync(stream);
                }catch
                {
                    newFileName = "default.png";
                }

                animalForm.Animal.ImageName = newFileName;

                try
                {
                    animalService.AddAnimal(animalForm.Animal);
                }catch
                {
                    System.IO.File.Delete(path);
                }

                return RedirectToAction("Index");
            }

            
            return View("NewAnimal",animalForm);
        }
    }
}
