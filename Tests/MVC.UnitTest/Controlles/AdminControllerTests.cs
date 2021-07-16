using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Auth;
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
    public class AdminControllerTests
    {
        private Mock<ICategoryService> cateService;
        private Mock<IAnimalService> aniService;
        private Mock<IAuthService> authService;
        private Mock<IMapper> mapper;
        private readonly IConfiguration configuration;
        private DataEntities entities;

        public AdminControllerTests()
        {

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"DefaultImageId","1"}
                })
                .Build();
        }

        [TestInitialize]
        public void Init()
        {
            cateService = new Mock<ICategoryService>();
            aniService = new Mock<IAnimalService>();
            authService = new Mock<IAuthService>();
            mapper = new Mock<IMapper>();
            entities = new DataEntities();           
        }

        [TestMethod]
        public void AdminControllerTests_Index_ReturnView()
        {
            // Arrange
            cateService.Setup(ser => ser.GetCategorys())
                .Returns(entities.GetCategorys());
            var expect = entities.GetCategoryViewModels();
            mapper.Setup(m => m.Map<IEnumerable<CategoryViewModel>>(It.IsAny<IEnumerable<Category>>()))
                .Returns(expect);

            var controller = new AdminController(cateService.Object,
                authService.Object,
                configuration,
                aniService.Object,
                mapper.Object);
            // Act

            var result = controller.Index();

            // Assert

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;

            Assert.AreEqual(viewResult.Model, expect);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-4)]
        public void AdminControllerTests_DeleteAnimal_CallWith0_ReturnRedirect(int input)
        {
            // Arrange
            //var input = 0;
            aniService.Setup(ser => ser.GetAnimal(It.IsAny<int>()))
                .Returns<int>(inp=>entities.GetAnimals().FirstOrDefault(ani => ani.AnimalId == inp));
            aniService.Setup(ser => ser.DeleteAnimal(It.IsAny<int>()));

            var controller = new AdminController(cateService.Object,
                authService.Object,
                configuration,
                aniService.Object,
                mapper.Object);

            // Act

            var result = controller.DeleteAnimal(input);

            // Assert
            aniService.Verify(ser => ser.GetAnimal(input), Times.Once());
            aniService.Verify(ser => ser.DeleteAnimal(input), Times.Never());
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var viewResult = result as RedirectToActionResult;

            Assert.IsNotNull(viewResult);
            Assert.AreEqual(viewResult.ActionName, "Index");
        }

        [DataTestMethod]
        [DataRow(3)]
        [DataRow(2)]
        [DataRow(1)]
        public void AdminControllerTests_DeleteAnimal_CallWithId_ReturnRedirect(int input)
        {
            // Arrange
            //var input = 1;
            aniService.Setup(ser => ser.GetAnimal(It.IsAny<int>()))
                .Returns<int>(inp => entities.GetAnimals().FirstOrDefault(ani => ani.AnimalId == inp));
            aniService.Setup(ser => ser.DeleteAnimal(It.IsAny<int>()));

            var controller = new AdminController(cateService.Object,
                authService.Object,
                configuration,
                aniService.Object,
                mapper.Object);

            // Act

            var result = controller.DeleteAnimal(input);

            // Assert
            aniService.Verify(ser => ser.GetAnimal(input), Times.Once());
            aniService.Verify(ser => ser.DeleteAnimal(input), Times.Once());
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var viewResult = result as RedirectToActionResult;

            Assert.IsNotNull(viewResult);
            Assert.AreEqual(viewResult.ActionName, "Index");
        }


        [DataTestMethod]
        [DataRow(-3)]
        [DataRow(-2)]
        [DataRow(0)]
        public void AdminControllerTests_AnimalForm_CallWith_ReturnRedirect(int input)
        {
            // Arrange
            //var input = 1;
            aniService.Setup(ser => ser.AddAnimal(It.IsAny<Animal>()));
            var inputanimal = new AnimalViewModel
            {
                AnimalId = 0,
                Name = "test"
            };
            var exp = new Animal();

            mapper.Setup(ser => ser.Map<Animal>(It.IsAny<AnimalViewModel>()))
                .Returns<AnimalViewModel>(inp => 
                {
                    exp.AnimalId = inp.AnimalId;
                    exp.Name = inp.Name;
                    return exp;
                }); 

            var controller = new AdminController(cateService.Object,
                authService.Object,
                configuration,
                aniService.Object,
                mapper.Object);

            // Act

            var result = controller.AnimalForm(inputanimal,input);

            // Assert
            mapper.Verify(map => map.Map<Animal>(inputanimal), Times.Once());
            aniService.Verify(ser => ser.AddAnimal(exp), Times.Once());
            Assert.AreEqual(input, exp.AnimalId);
            Assert.AreEqual("test", exp.Name);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var viewResult = result as RedirectToActionResult;

            Assert.IsNotNull(viewResult);
            Assert.AreEqual(viewResult.ActionName, "Index");
        }

        [TestMethod]
        public void AdminControllerTests_AnimalForm_CallWithId_ReturnRedirect()
        {
            // Arrange
            var input = 1;
            aniService.Setup(ser => ser.EditAnimal(It.IsAny<Animal>()));
            var inputanimal = new AnimalViewModel
            {
                AnimalId = 0,
                Name = "test"
            };
            var exp = new Animal();

            mapper.Setup(ser => ser.Map<Animal>(It.IsAny<AnimalViewModel>()))
                .Returns<AnimalViewModel>(inp =>
                {
                    exp.ImageId = inp.ImageId;
                    exp.Name = inp.Name;
                    return exp;
                });

            var controller = new AdminController(cateService.Object,
                authService.Object,
                configuration,
                aniService.Object,
                mapper.Object);

            // Act

            var result = controller.AnimalForm(inputanimal, input);

            // Assert
            mapper.Verify(map => map.Map<Animal>(inputanimal), Times.Once());
            aniService.Verify(ser => ser.EditAnimal(exp), Times.Once());
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var viewResult = result as RedirectToActionResult;

            Assert.IsNotNull(viewResult);
            Assert.AreEqual(viewResult.ActionName, "Index");
        }

    }
}
