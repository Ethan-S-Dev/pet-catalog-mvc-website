using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCatalog.Domain.Models;
using PetCatalog.Infra.Data.Contexts;
using PetCatalog.Infra.Data.Interfaces;
using PetCatalog.Infra.Data.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitTest.Repositorys
{
    [TestClass]
    public class ImageRepositoryTests
    {
        private PetCatalogDbContext dbContext;
        private Mock<IConfiguration> config;
        private Mock<IFileContext> fileContext;

        [TestInitialize]
        public void Init()
        {
            config = new Mock<IConfiguration>();
            fileContext = new Mock<IFileContext>();
            config.Setup(cfg => cfg["DefaultImageName"]).Returns("test");
            config.Setup(cfg => cfg["DefaultImageId"]).Returns("1");
            var dboptions = new DbContextOptionsBuilder<PetCatalogDbContext>();
            dboptions.UseInMemoryDatabase("testDb");

            dbContext = new PetCatalogDbContext(dboptions.Options, config.Object);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [TestMethod]
        public void ImageRepositoryTests_Create_CallWithImage()
        {
            // Arrange
            var input = new Image()
            {
                ImageId = 5,
                Name = "imagename.png",
                Data = new byte[] { 1, 2, 3, 4, 5, 43 }
            };

            fileContext.Setup(fs => fs.Save(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(true);

            var rep = new ImageRepository(fileContext.Object, dbContext);

            // Act
            rep.Create(input);

            // Assert
            var result = dbContext.Images.FirstOrDefault(im=>im.ImageId == 5);
            fileContext.Verify(fs => fs.Save(It.IsAny<string>(), input.Data), Times.Exactly(1));
            Assert.IsNotNull(result);
            Assert.AreNotEqual("imagename.png", result.Name);
        }

        [TestMethod]
        public void ImageRepositoryTests_Delete_CallWithImageId_ReturnImage()
        {
            // Arrange
            var input = 6;
            var image = new Image()
            {
                ImageId = input,
                Name = "imagename.png",
                Data = new byte[] { 1, 2, 3, 4, 5, 43 }
            };

            fileContext.Setup(fs => fs.Delete(It.IsAny<string>()))
                .Returns(true);
            dbContext.Images.Add(image);
            dbContext.SaveChanges();

            var rep = new ImageRepository(fileContext.Object, dbContext);

            // Act
            var result = rep.Delete(input);

            // Assert
            var noimage = dbContext.Images.FirstOrDefault(im => im.ImageId == input);
            fileContext.Verify(fs => fs.Delete("imagename.png"), Times.Exactly(1));
            Assert.IsNull(noimage);
            Assert.IsNotNull(result);            
        }

        [TestMethod]
        public void ImageRepositoryTests_Get_CallWithImageId_ReturnImage()
        {
            // Arrange
            var input = 7;
            var image = new Image()
            {
                ImageId = input,
                Name = "imagename.png",
                Data = new byte[] { 1, 2, 3, 4, 5, 43 }
            };

            dbContext.Images.Add(image);
            dbContext.SaveChanges();

            var rep = new ImageRepository(fileContext.Object, dbContext);

            // Act
            var result = rep.Get(input);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ImageRepositoryTests_GetAll_ReturnImage()
        {
            // Arrange
            var image = new Image()
            {
                ImageId = 8,
                Name = "imagename.png",
                Data = new byte[] { 1, 2, 3, 4, 5, 43 }
            };
            var image2 = new Image()
            {
                ImageId = 9,
                Name = "imagename.png",
                Data = new byte[] { 1, 2, 3, 4, 5, 43 }
            };

            dbContext.Images.Add(image);
            dbContext.Images.Add(image2);
            dbContext.SaveChanges();

            var rep = new ImageRepository(fileContext.Object, dbContext);

            // Act
            var result = rep.GetAll().ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void ImageRepositoryTests_Update()
        {
            // Arrange
            var image = new Image()
            {
                ImageId = 8,
                Name = "imagename.png",
                Data = new byte[] { 1, 2, 3, 4, 5, 43 }
            };
            var input = new Image()
            {
                ImageId = 8,
                Name = "imagename2.png",
                Data = new byte[] { 1, 2, 3, 4, 5, 43 ,43,55,34}
            };

            dbContext.Images.Add(image);
            dbContext.SaveChanges();
            fileContext.Setup(fs => fs.Update(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<byte[]>()))
                .Returns(true);
            var rep = new ImageRepository(fileContext.Object, dbContext);

            // Act
            rep.Update(input);

            // Assert
            fileContext.Verify(fs => fs.Update("imagename.png", It.IsAny<string>(), input.Data),Times.Exactly(1));
            var result = dbContext.Images.Find(8);
            Assert.IsNotNull(result);
            Assert.AreNotEqual("imagename2.png", result.Name);

        }
    }
}
