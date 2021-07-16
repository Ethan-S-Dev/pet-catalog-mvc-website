using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCatalog.Application.Interfaces;
using PetCatalog.MVC.Controllers;
using Shared.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.UnitTest.Controlles
{
    [TestClass]
    public class ErrorControllerTests
    {

        [TestMethod]
        public void ErrorControllerTests_Index_ReturnViewResult()
        {
            // Arrange

            var controller = new ErrorController();
            // Act

            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult);

        }
    }
}
