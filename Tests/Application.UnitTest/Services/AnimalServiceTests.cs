using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCatalog.Application.Services;
using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using Shared.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Services
{
    [TestClass]
    public class AnimalServiceTests
    {
        private Mock<IAnimalRepository> animalRepository;
        private Mock<IImageRepository> imageRepository;
        private Mock<ICommentRepository> commentRepository;
        private Mock<ICategoryRepository> categoryRepository;
        private DataEntities dataEntities;

        public AnimalServiceTests()
        {
           
        }

        [TestInitialize]
        public void Init()
        {
            animalRepository = new Mock<IAnimalRepository>();
            imageRepository = new Mock<IImageRepository>();
            commentRepository = new Mock<ICommentRepository>();
            categoryRepository = new Mock<ICategoryRepository>();
            dataEntities = new DataEntities();
        }

        [TestMethod]
        public void AnimalServiceTests_AddAnimal_CallWithAnimalAndImageAndUseCate_ReturnBool()
        {
            // Arrange
            var input = dataEntities.GetAnimals().First();
            var imgid = input.Image.ImageId;

            imageRepository.Setup(rep => rep.Get(It.IsAny<int>()))
                .Returns(dataEntities.GetImages().First(img => img.ImageId == input.ImageId));
            animalRepository.Setup(rep => rep.Create(It.IsAny<Animal>()));

            var service = new AnimalService(animalRepository.Object,imageRepository.Object, commentRepository.Object,categoryRepository.Object);

            var expected = true;

            // Act

            var result = service.AddAnimal(input);

            // Assert

            imageRepository.Verify(rep => rep.Get(imgid), Times.Exactly(1));
            animalRepository.Verify(rep => rep.Create(input), Times.Exactly(1));
            categoryRepository.Verify(rep => rep.GetAll(), Times.Never());
            categoryRepository.Verify(rep => rep.Create(It.IsAny<Category>()), Times.Never());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AnimalServiceTests_AddAnimal_CallWithAnimalNoImageNewCate_ReturnBool()
        {
            // Arrange
            //Image nullimage = null;

            var input = dataEntities.GetAnimals().First();
            input.Image.ImageId = 0;
            input.CategoryId = -1;
            input.Category = new Category() { Name = "test" };

            imageRepository.Setup(rep => rep.Get(It.IsAny<int>()));
              
            imageRepository.Setup(rep => rep.Create(It.IsAny<Image>()));

            animalRepository.Setup(rep => rep.Create(It.IsAny<Animal>()));

            categoryRepository.Setup(rep => rep.GetAll());

            categoryRepository.Setup(rep => rep.Create(It.IsAny<Category>())).Callback<Category>(c =>
            {
                c.CategoryId = 1;
            });

            var service = new AnimalService(animalRepository.Object, imageRepository.Object,commentRepository.Object, categoryRepository.Object);

            var expected = true;

            // Act

            var result = service.AddAnimal(input);

            // Assert

            imageRepository.Verify(rep => rep.Get(0), Times.Exactly(1));
            imageRepository.Verify(rep => rep.Create(input.Image), Times.Exactly(1));
            animalRepository.Verify(rep => rep.Create(input), Times.Exactly(1));
            categoryRepository.Verify(rep => rep.GetAll(), Times.Exactly(1));
            categoryRepository.Verify(rep => rep.Create(It.IsAny<Category>()), Times.Exactly(1));

            Assert.AreEqual(input.CategoryId, 1);
            Assert.AreEqual(input.Category.CategoryId, 1);
            Assert.AreEqual(input.Category.Name, "test");
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void AnimalServiceTests_DeleteAnimal_CallWithAnimalID()
        {
            // Arrange
            var input = dataEntities.GetAnimals().First();

            imageRepository.Setup(rep => rep.Delete(It.IsAny<int>()));
            commentRepository.Setup(rep => rep.DeleteAnimalComments(It.IsAny<int>()));
            animalRepository.Setup(rep => rep.Delete(It.IsAny<int>()))
                .Returns<int>(e=> dataEntities.GetAnimals().First(a=>a.AnimalId == e));

            var service = new AnimalService(animalRepository.Object, imageRepository.Object, commentRepository.Object, categoryRepository.Object);

            // Act

            service.DeleteAnimal(input.AnimalId);

            // Assert

            imageRepository.Verify(rep => rep.Delete(input.ImageId), Times.Exactly(1));
            animalRepository.Verify(rep => rep.Delete(input.AnimalId), Times.Exactly(1));
            commentRepository.Verify(rep => rep.DeleteAnimalComments(input.AnimalId), Times.Exactly(1));
            
        }


        [TestMethod]
        public void AnimalServiceTests_EditAnimal_CallWithAnimalNoImageNewCate()
        {
            // Arrange
            var input = dataEntities.GetAnimals().First();
            var realanimal = new DataEntities().GetAnimals().First(a => a.AnimalId == input.AnimalId);

            animalRepository.Setup(rep => rep.Get(It.IsAny<int>()))
                .Returns(realanimal);

            animalRepository.Setup(rep => rep.Update(It.IsAny<Animal>()));

            imageRepository.Setup(rep => rep.Update(It.IsAny<Image>()))
                .Callback<Image>(a =>
                {
                    a.ImageId = realanimal.Image.ImageId;
                });

            input.Image.ImageId = 0;
            input.ImageId = 0;

            input.CategoryId = -1;
            input.Category = new Category() { Name = "test" };

            categoryRepository.Setup(rep => rep.GetAll());

            categoryRepository.Setup(rep => rep.Create(It.IsAny<Category>())).Callback<Category>(c =>
            {
                c.CategoryId = 1;
            });

            var service = new AnimalService(animalRepository.Object, imageRepository.Object,commentRepository.Object, categoryRepository.Object);

            // Act

            service.EditAnimal(input);

            // Assert

            imageRepository.Verify(rep => rep.Update(input.Image), Times.Exactly(1));
            animalRepository.Verify(rep => rep.Get(input.AnimalId), Times.Exactly(1));
            animalRepository.Verify(rep => rep.Update(input), Times.Exactly(1));
            categoryRepository.Verify(rep => rep.GetAll(), Times.Exactly(1));
            categoryRepository.Verify(rep => rep.Create(It.IsAny<Category>()), Times.Exactly(1));

            Assert.AreEqual(input.CategoryId, 1);
            Assert.AreEqual(input.Category.CategoryId, 1);
            Assert.AreEqual(input.Category.Name, "test");

            Assert.AreEqual(realanimal.Image.ImageId, input.Image.ImageId);
            Assert.AreEqual(realanimal.Image.ImageId, input.ImageId);

        }

        [TestMethod]
        public void AnimalServiceTests_EditAnimal_CallWithAnimalWithImageUseCate()
        {
            // Arrange
            var input = dataEntities.GetAnimals().First();
            var realanimal = new DataEntities().GetAnimals().First(a => a.AnimalId == input.AnimalId);

            animalRepository.Setup(rep => rep.Get(It.IsAny<int>()))
                .Returns(realanimal);

            animalRepository.Setup(rep => rep.Update(It.IsAny<Animal>()));

            var service = new AnimalService(animalRepository.Object, imageRepository.Object, commentRepository.Object, categoryRepository.Object);

            // Act

            service.EditAnimal(input);

            // Assert

            animalRepository.Verify(rep => rep.Get(input.AnimalId), Times.Exactly(1));
            animalRepository.Verify(rep => rep.Update(input), Times.Exactly(1));
            categoryRepository.Verify(rep => rep.GetAll(), Times.Never());
            categoryRepository.Verify(rep => rep.Create(It.IsAny<Category>()), Times.Never());

        }

        [TestMethod]
        public void AnimalServiceTests_GetAllAnimals_ReturnsAnimals()
        {
            // Arrange
            var input = dataEntities.GetAnimals().First();
            var realanimals = new DataEntities().GetAnimals();

            animalRepository.Setup(rep => rep.GetAll())
                .Returns(realanimals);

            var service = new AnimalService(animalRepository.Object, imageRepository.Object, commentRepository.Object, categoryRepository.Object);

            // Act

            service.GetAllAnimals();

            // Assert

            animalRepository.Verify(rep => rep.GetAll(), Times.Exactly(1));

        }

        [TestMethod]
        public void AnimalServiceTests_GetAnimal__CallWithAnimalId_ReturnsAnimal()
        {
            // Arrange
            var input = dataEntities.GetAnimals().First();
            var realanimals = new DataEntities().GetAnimals();

            animalRepository.Setup(rep => rep.Get(It.IsAny<int>()))
                .Returns<int>(a=> realanimals.First(d=>d.AnimalId == a));

            var service = new AnimalService(animalRepository.Object, imageRepository.Object, commentRepository.Object, categoryRepository.Object);

            // Act

            var result = service.GetAnimal(input.AnimalId);

            // Assert

            animalRepository.Verify(rep => rep.Get(input.AnimalId), Times.Exactly(1));

            DataEntities.AssertAnimals(input, result);

        }

        [TestMethod]
        public void AnimalServiceTests_GetBestAnimals_ReturnsTwoAnimals()
        {
            // Arrange
            var expected = dataEntities.GetAnimals().OrderByDescending(ani=>ani.Comments.Count()).Take(2);
            var realanimals = new DataEntities().GetAnimals().OrderByDescending(ani => ani.Comments.Count()).Take(2);

            animalRepository.Setup(rep => rep.GetTopCommented())
                .Returns(realanimals);

            var service = new AnimalService(animalRepository.Object, imageRepository.Object, commentRepository.Object, categoryRepository.Object);

            // Act

            var result = service.GetBestAnimals();

            // Assert

            animalRepository.Verify(rep => rep.GetTopCommented(), Times.Exactly(1));

            Assert.AreEqual(expected.Count(), result.Count());

            for (int i = 0; i < result.Count(); i++)
            {
                DataEntities.AssertAnimals(expected.ElementAt(i), result.ElementAt(i));
            }

        }

        [TestMethod]
        public void AnimalServiceTests_GetEmptyAnimal_ReturnsTwoAnimals()
        {
            // Arrange
            var image = new Image()
            {
                ImageId = 1,
                Name = "default.png",
                Data = null
            };
            
            var expected = new Animal()
            {
                Image = image
            };

            var realanimals = new DataEntities().GetAnimals().OrderByDescending(ani => ani.Comments.Count()).Take(2);

            imageRepository.Setup(rep => rep.Get(It.IsAny<int>()))
                .Returns(image);

            var service = new AnimalService(animalRepository.Object, imageRepository.Object, commentRepository.Object, categoryRepository.Object);

            // Act

            var result = service.GetEmptyAnimal();

            // Assert

            imageRepository.Verify(rep => rep.Get(1), Times.Exactly(1));
            DataEntities.AssertImage(image, result.Image);

        }

        [TestMethod]
        public void AnimalServiceTests_AddComment_CallWithComment()
        {
            
            // Arrange
            var input = new Comment()
            {
                AnimalId = 1,
                Value = "dddd"
            };

            commentRepository.Setup(rep => rep.Create(It.IsAny<Comment>()));

            var service = new AnimalService(animalRepository.Object, imageRepository.Object, commentRepository.Object, categoryRepository.Object);

            // Act

            service.AddComment(input);

            // Assert

            commentRepository.Verify(rep => rep.Create(input),Times.Exactly(1));


        }

        [TestMethod]
        public void AnimalServiceTests_DeleteComment_CallWithCommentId()
        {

            // Arrange
            var input = 1;

            commentRepository.Setup(rep => rep.Delete(It.IsAny<int>()));

            var service = new AnimalService(animalRepository.Object, imageRepository.Object, commentRepository.Object, categoryRepository.Object);

            // Act

            service.DeleteComment(input);

            // Assert

            commentRepository.Verify(rep => rep.Delete(input), Times.Exactly(1));


        }

    }
}
