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
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> categoryRepository;
        private DataEntities entitys;

        [TestInitialize]
        public void Init()
        {
            categoryRepository = new Mock<ICategoryRepository>();
            entitys = new DataEntities();
        }

        [TestMethod]
        public void CategoryServiceTests_GetAnimals_CallWithCateId_ReturnAnimals()
        {
            // Arrange
            var animals = entitys.GetAnimals().OrderByDescending(ani => ani.Name.ToLower()).ToList();
            var ret = new Category()
            {
                Animals = animals
            };
            categoryRepository.Setup(rep => rep.Get(It.IsAny<int>())).Returns(ret);

            var input = 1;

            var service = new CategoryService(categoryRepository.Object);

            var expected = entitys.GetAnimals().OrderBy(ani => ani.Name.ToLower()).ToList();

            // Act

            var result = service.GetAnimals(1);

            // Assert

            categoryRepository.Verify(rep => rep.Get(input), Times.Exactly(1));
            Assert.AreEqual(expected.Count(), result.Count());

            for (int i = 0; i < expected.Count(); i++)
            {
                DataEntities.AssertAnimals(expected.ElementAt(i), result.ElementAt(i));
            }

        }

        [TestMethod]
        public void CategoryServiceTests_GetCategory_CallWithCateId_ReturnCategory()
        {
            // Arrange
            var animals = entitys.GetAnimals().OrderByDescending(ani => ani.Name.ToLower()).ToList();
            var expected = new Category()
            {
                Animals = animals
            };
            categoryRepository.Setup(rep => rep.Get(It.IsAny<int>())).Returns(expected);

            var input = 1;

            var service = new CategoryService(categoryRepository.Object);

            // Act

            var result = service.GetCategory(1);

            // Assert

            categoryRepository.Verify(rep => rep.Get(input), Times.Exactly(1));
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void CategoryServiceTests_GetCategorys_ReturnCategorys()
        {
            // Arrange
            var expected = entitys.GetCategorys();

            categoryRepository.Setup(rep => rep.GetAll()).Returns(expected);

            var service = new CategoryService(categoryRepository.Object);

            // Act

            var result = service.GetCategorys();

            // Assert

            categoryRepository.Verify(rep => rep.GetAll(), Times.Exactly(1));
            Assert.AreEqual(expected.Count(), result.Count());

            for (int i = 0; i < expected.Count(); i++)
            {
                DataEntities.AssertCategory(expected.ElementAt(i), result.ElementAt(i));
            }

        }
    }
}
