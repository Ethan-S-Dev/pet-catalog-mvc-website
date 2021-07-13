using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.MVC.Controllers;
using PetCatalog.MVC.ViewModels;
using Shared.UnitTest;
using System.Collections.Generic;
using System.Linq;


namespace MVC.UnitTest.Controlles
{
    [TestClass]
    public class HomeControllerTest
    {
        private readonly Mock<IAnimalService> aniService;
        private readonly Mock<IMapper> mapper;

        public HomeControllerTest()
        {
            aniService = new Mock<IAnimalService>();
            mapper = new Mock<IMapper>();
        }


        [TestMethod]
        public void HomeControllerTest_Index_ReturnViewResult()
        {
            // Arrange
            var entitys = new DataEntities();

            var bestanimals = entitys.GetAnimals().OrderByDescending(ani => ani.Comments.Count()).Take(2);

            aniService.Setup(s => s.GetBestAnimals())
                 .Returns(bestanimals);
            
            var expected = entitys.GetAnimalViewModels().OrderByDescending(ani => ani.Comments.Count()).Take(2);

            mapper.Setup(m => m.Map<IEnumerable<AnimalViewModel>>(bestanimals))
                .Returns(expected);


            var controller = new HomeController(aniService.Object, mapper.Object);
            // Act

            var result = controller.Index();

            // Assert

            aniService.Verify(m => m.GetBestAnimals(), Times.Exactly(1));
            mapper.Verify(m => m.Map<IEnumerable<AnimalViewModel>>(bestanimals), Times.Exactly(1));

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;

            Assert.AreEqual(viewResult.Model, expected);

        }
    }
}
