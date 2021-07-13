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
        private DataEntities dataEntities;

        public AnimalServiceTests()
        {
           
        }

        [TestInitialize]
        public void Init()
        {
            animalRepository = new Mock<IAnimalRepository>();
            imageRepository = new Mock<IImageRepository>();
            dataEntities = new DataEntities();
        }

        [TestMethod]
        public void AnimalServiceTests_AddAnimal_CallWithAnimalAndImage_ReturnBool()
        {
            // Arrange
            var input = dataEntities.GetAnimals().First();
            var imgid = input.Image.ImageId;

            imageRepository.Setup(rep => rep.Get(It.IsAny<int>()))
                .Returns(dataEntities.GetImages().First(img => img.ImageId == input.ImageId));
            animalRepository.Setup(rep => rep.Create(It.IsAny<Animal>()));

            var service = new AnimalService(animalRepository.Object,imageRepository.Object);

            var expected = true;

            // Act

            var result = service.AddAnimal(input);

            // Assert

            imageRepository.Verify(rep => rep.Get(imgid), Times.Exactly(1));
            animalRepository.Verify(rep => rep.Create(input), Times.Exactly(1));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AnimalServiceTests_AddAnimal_CallWithAnimalNoImage_ReturnBool()
        {
            // Arrange
            //Image nullimage = null;

            var input = dataEntities.GetAnimals().First();
            input.Image.ImageId = 0;

            imageRepository.Setup(rep => rep.Get(It.IsAny<int>()));
              
            imageRepository.Setup(rep => rep.Create(It.IsAny<Image>()));

            animalRepository.Setup(rep => rep.Create(It.IsAny<Animal>()));

            var service = new AnimalService(animalRepository.Object, imageRepository.Object);

            var expected = true;

            // Act

            var result = service.AddAnimal(input);

            // Assert

            imageRepository.Verify(rep => rep.Get(0), Times.Exactly(1));
            imageRepository.Verify(rep => rep.Create(input.Image), Times.Exactly(1));
            animalRepository.Verify(rep => rep.Create(input), Times.Exactly(1));

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void AnimalServiceTests_DeleteAnimal_CallWithAnimalID()
        {
            // Arrange
            var input = dataEntities.GetAnimals().First();

            imageRepository.Setup(rep => rep.Delete(It.IsAny<int>()));
            animalRepository.Setup(rep => rep.Delete(It.IsAny<int>()))
                .Returns<int>(e=> dataEntities.GetAnimals().First(a=>a.AnimalId == e));

            var service = new AnimalService(animalRepository.Object, imageRepository.Object);

            // Act

            service.DeleteAnimal(input.AnimalId);

            // Assert

            imageRepository.Verify(rep => rep.Delete(input.ImageId), Times.Exactly(1));
            animalRepository.Verify(rep => rep.Delete(input.AnimalId), Times.Exactly(1));
            
        }


        [TestMethod]
        public void AnimalServiceTests_EditAnimal_CallWithAnimal()
        {
            // Arrange
            var input = dataEntities.GetAnimals().First();

            imageRepository.Setup(rep => rep.Update(It.IsAny<Image>()));

            animalRepository.Setup(rep => rep.Get(It.IsAny<int>()))
                .Returns<int>(e => dataEntities.GetAnimals().First(a => a.AnimalId == e));

            var service = new AnimalService(animalRepository.Object, imageRepository.Object);

            // Act

            service.DeleteAnimal(input.AnimalId);

            // Assert

            imageRepository.Verify(rep => rep.Delete(input.ImageId), Times.Exactly(1));
            animalRepository.Verify(rep => rep.Delete(input.AnimalId), Times.Exactly(1));

        }

    }
}
