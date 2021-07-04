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
        private readonly ICommentService commentService;
        private readonly ICategoryService categoryService;
        private readonly IAnimalService animalService;
        public AdminController(IWebHostEnvironment webHostEnvironment, ICategoryService categoryService, IAnimalService animalService, ICommentService commentService)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.commentService = commentService;
            this.categoryService = categoryService;
            this.animalService = animalService;
        }

        public IActionResult Index()
        {
            var model = categoryService.GetCategorys();
            return View(model);
        }

        public IActionResult NewAnimal()
        {
            return View(new AnimalFormModel(categoryService));
        }

        [HttpGet]
        public IActionResult EditAnimal(int id)
        {
            if (id == 0) return RedirectToAction("Index");
            var animal = animalService.GetAnimal(id);
            return View(new AnimalEditModel(categoryService)
            {
                Animal = animal,
                CategoryId = animal.CategoryId,
                CategoryName = animal.Category.Name,
                Image = new FormFile(null, 0, 0, animal.PictureName, animal.PictureName)
            });
        }

        public IActionResult DeleteAnimal(int id)
        {
            if (id == 0) return RedirectToAction("Index");
            var animal = animalService.GetAnimal(id);

            var oldPic = Path.Combine(webHostEnvironment.WebRootPath, "res/images/animals", animal.PictureName);
            System.IO.File.Delete(oldPic);

            commentService.DeleteComments(id);
            animalService.DeleteAnimal(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAnimal(AnimalFormModel animalForm)
        {
            if (ModelState.IsValid)
            {
                if (animalForm.CategoryId == -1)
                {
                    if (!categoryService.AddCategory(animalForm.CategoryName, out int id))
                        return View("NewAnimal");

                    animalForm.Animal.CategoryId = id;

                }
                else
                    animalForm.Animal.CategoryId = animalForm.CategoryId;

                var unsafeFileName = Path.GetFileName(animalForm.Image.FileName);
                var exten = Path.GetExtension(unsafeFileName);
                var newFileName = $"{Guid.NewGuid()}{exten}";
                var path = Path.Combine(webHostEnvironment.WebRootPath, "res/images/animals", newFileName);
                try
                {

                    using (var stream = new FileStream(path, FileMode.Create))
                        animalForm.Image.CopyToAsync(stream);
                }
                catch
                {
                    newFileName = "default.png";
                }

                animalForm.Animal.PictureName = newFileName;

                try
                {
                    animalService.AddAnimal(animalForm.Animal);
                }
                catch
                {
                    System.IO.File.Delete(path);
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
                if (animalForm.CategoryId == -1)
                {
                    if (!categoryService.AddCategory(animalForm.CategoryName, out int id))
                        return View("NewAnimal");

                    animalForm.Animal.CategoryId = id;

                }
                else
                    animalForm.Animal.CategoryId = animalForm.CategoryId;

                if (animalForm.Image is not null)
                {
                    var unsafeFileName = Path.GetFileName(animalForm.Image.FileName);
                    var exten = Path.GetExtension(unsafeFileName);
                    var newFileName = $"{Guid.NewGuid()}{exten}";
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "res/images/animals", newFileName);
                    var oldPic = Path.Combine(webHostEnvironment.WebRootPath, "res/images/animals", animalForm.Animal.PictureName);
                    try
                    {

                        using (var stream = new FileStream(path, FileMode.Create))
                            animalForm.Image.CopyToAsync(stream);

                        System.IO.File.Delete(oldPic);
                    }
                    catch
                    {
                        newFileName = animalForm.Animal.PictureName;
                    }

                    animalForm.Animal.PictureName = newFileName;


                }

                animalService.EditAnimal(animalForm.Animal);

                return RedirectToAction("Index");
            }

            animalForm.Categorys = categoryService.GetCategorys();
            IActionResult result = View("EditAnimal", animalForm);
            return result;
        }
    }
}
