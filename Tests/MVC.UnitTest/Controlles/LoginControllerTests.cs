using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCatalog.Application.Interfaces;
using PetCatalog.MVC.Controllers;
using PetCatalog.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.UnitTest.Controlles
{
    [TestClass]
    public class LoginControllerTests
    {
        private Mock<IMapper> mapper;
        private Mock<IAuthService> authService;

        [TestInitialize]
        public void Init()
        {
            mapper = new Mock<IMapper>();
            authService = new Mock<IAuthService>();
        }

        [TestMethod]
        public void LoginControllerTests_Index_CallWithPath_ReturnView()
        {
            // Arrange
            var input = "test/path";
            var controller = new LoginController(authService.Object,mapper.Object);
            // Act
            var result = controller.Index(input);
            // Assert

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var res = result as ViewResult;
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Model);
            Assert.IsInstanceOfType(res.Model, typeof(LoginViewModel));
            var model = res.Model as LoginViewModel;
            Assert.AreEqual("test/path", model.RedirectPath);
        }
    }
}
