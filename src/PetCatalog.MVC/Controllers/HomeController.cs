using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.MVC.ViewModels;
using System.Collections.Generic;

namespace PetCatalog.MVC.Controllers
{

    public class HomeController : Controller
    {
        private readonly IAnimalService animalService;
        private readonly IMapper mapper;
        

        public HomeController(IAnimalService animalService, IMapper mapper)
        {
            this.animalService = animalService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var animals = animalService.GetBestAnimals();
            var bestAnimals = mapper.Map<IEnumerable<AnimalViewModel>>(animals);
            return View(bestAnimals);
        }

        [HttpPost]
        public IActionResult AddComment(CommentViewModel comment, int id)
        {
            if (ModelState.IsValid)
            {
                comment.AnimalId = id;
                var realComment = mapper.Map<Comment>(comment);
                animalService.AddComment(realComment);
            }
            var url = Request?.Headers["Referer"].ToString();
            url ??= "/";
            return Redirect(url);
        }
    }
}
