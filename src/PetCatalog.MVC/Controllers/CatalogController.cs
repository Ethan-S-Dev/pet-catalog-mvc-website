using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCatalog.Application.Interfaces;
using PetCatalog.MVC.ViewModels;
using System.Collections.Generic;

namespace PetCatalog.MVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        public CatalogController(ICategoryService categoryService,IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {         
             var model = mapper.Map<IEnumerable<CategoryViewModel>>(categoryService.GetCategorys());         
            return View(model);
        }

    }
}
