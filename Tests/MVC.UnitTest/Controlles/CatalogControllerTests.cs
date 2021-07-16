using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.MVC.Controllers;
using PetCatalog.MVC.ViewModels;
using Shared.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.UnitTest.Controlles
{
    [TestClass]
    public class CatalogControllerTests
    {
        private Mock<ICategoryService> cateService;
        private Mock<IMapper> mapper;


        [TestInitialize]
        public void Init()
        {
            cateService = new Mock<ICategoryService>();
            mapper = new Mock<IMapper>();
        }


        [TestMethod]
        public void CatalogControllerTests_Index_ReturnViewResult()
        {
            // Arrange
            var entitys = new DataEntities();

            var mapperinp = entitys.GetCategorys().OrderBy(cat => cat.Name.ToLower());
            var expact = entitys.GetCategoryViewModels().OrderBy(cat => cat.Name.ToLower());

            cateService.Setup(s => s.GetCategorys())
                 .Returns(mapperinp);        

            mapper.Setup(m => m.Map<IEnumerable<CategoryViewModel>>(It.IsAny<IEnumerable<Category>>()))
                .Returns(expact);


            var controller = new CatalogController(cateService.Object, mapper.Object);
            // Act

            var result = controller.Index();

            // Assert

            cateService.Verify(m => m.GetCategorys(), Times.Exactly(1));
            mapper.Verify(m => m.Map<IEnumerable<CategoryViewModel>>(mapperinp), Times.Exactly(1));

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;

            Assert.AreEqual(viewResult.Model, expact);

        }      
    }
}
