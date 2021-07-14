using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCatalog.Application.Services;
using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Services
{
    [TestClass]
    public class ImageServiceTests
    {
        private Mock<IImageRepository> imageRepository;

        [TestInitialize]
        public void Init()
        {
            imageRepository = new Mock<IImageRepository>();
        }

        [TestMethod]
        public void ImageServiceTests_GetImageName_CallWithId_ReturnName()
        {
            // Arrange

            var ret = new Image()
            {
                Name = "test"
            };
            imageRepository.Setup(rep => rep.Get(It.IsAny<int>())).Returns(ret);

            var input = 6;

            var service = new ImageService(imageRepository.Object);

            // Act

            var result = service.GetImageName(input);

            // Assert

            imageRepository.Verify(rep => rep.Get(input), Times.Exactly(1));
            Assert.AreEqual("test", result);

        }
    }
}
