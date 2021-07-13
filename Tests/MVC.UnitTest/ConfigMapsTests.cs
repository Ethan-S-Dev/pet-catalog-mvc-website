using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using PetCatalog.Domain.Models;
using PetCatalog.MVC.Mappers;
using PetCatalog.MVC.ViewModels;
using System.Collections.Generic;
using Shared.UnitTest;

namespace MVC.UnitTest
{

    [TestClass]
    public class ConfigMapsTests
    {
        private readonly IMapper mapper;
        private DataEntities entitys;
        public ConfigMapsTests()
        {
            var configExpression = new MapperConfigurationExpression();
            configExpression.RegisterMaps();
            var config = new MapperConfiguration(configExpression);
            mapper = new Mapper(config);
        }

        [TestInitialize]
        public void Init()
        {
            entitys = new DataEntities();
        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        [TestMethod]
        public void ConfigMaps_Map_CallingMapperWithLoginViewModel_ReturnsUser()
        {
            // Arrange
            var loginIfo = new LoginViewModel()
            {
                Email = "Email@Email.com",
                Password = "pass",
                KeepLoggedIn = false,
                RedirectPath = null
            };

            var expected = new User()
            {
                UserId = 0,
                Name = null,
                Email = "Email@Email.com",
                Password = "pass"
            };

            // Act
            var result = mapper.Map<User>(loginIfo);

            // Assert

            Assert.AreEqual(expected.UserId, result.UserId);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Email, result.Email);
            Assert.AreEqual(expected.Password, result.Password);
        }

        [TestMethod]
        public void ConfigMaps_Map_CallingMapperWithComment_ReturnsCommentViewModel()
        {
            // Arrange
            var comment = entitys.GetComments().First(com => com.CommentId == 6);

            var expected = entitys.GetCommentViewModels().First(com => com.CommentId == 6);

            // Act
            var result = mapper.Map<CommentViewModel>(comment);

            // Assert

            AssertCommentsViewModel(expected, result);
        }

        [TestMethod]
        public void ConfigMaps_Map_CallingMapperWithCategory_ReturnsCategoryViewModel()
        {
            // Arrange
            var category = entitys.GetCategorys().First(ca => ca.CategoryId == 1);

            var expected = entitys.GetCategoryViewModels().First(ca => ca.CategoryId == 1);

            // Act
            var result = mapper.Map<CategoryViewModel>(category);

            // Assert

            AssertCategoryViewModel(expected, result);
        }

        [TestMethod]
        public void ConfigMaps_Map_CallingMapperWithAnimal_ReturnsAnimalViewModel()
        {
            // Arrange
            var animal = entitys.GetAnimals().First(ca => ca.AnimalId == 2);

            var expected = entitys.GetAnimalViewModels().First(ca => ca.AnimalId == 2);

            // Act
            var result = mapper.Map<AnimalViewModel>(animal);

            // Assert

            AssertAnimalsViewModel(expected, result);
        }


        [TestMethod]
        public void ConfigMaps_Map_CallingMapperWithCommentViewModel_ReturnsComment()
        {
            // Arrange
            var comment = entitys.GetCommentViewModels().First(com => com.CommentId == 4);

            var expected = entitys.GetComments().First(com => com.CommentId == 4);

            // Act
            var result = mapper.Map<Comment>(comment);

            // Assert

            AssertComments(expected, result);
        }

        [TestMethod]
        public void ConfigMaps_Map_CallingMapperWithCategoryViewModel_ReturnsCategory()
        {
            // Arrange
            var category = entitys.GetCategoryViewModels().First(ca => ca.CategoryId == 2);

            var expected = entitys.GetCategorys().First(ca => ca.CategoryId == 2);

            // Act
            var result = mapper.Map<Category>(category);

            // Assert

            AssertCategory(expected, result);
        }

        [TestMethod]
        public void ConfigMaps_Map_CallingMapperWithAnimalViewModel_ReturnsAnimal()
        {
            // Arrange
            var animal = entitys.GetAnimalViewModels().First(ca => ca.AnimalId == 3);

            var expected = entitys.GetAnimals().First(ca => ca.AnimalId == 3);

            // Act
            var result = mapper.Map<Animal>(animal);

            // Assert

            AssertAnimals(expected, result);
        }


        private void AssertCommentsViewModel(CommentViewModel expected,CommentViewModel result)
        {
            Assert.AreEqual(expected.CommentId, result.CommentId);
            Assert.AreEqual(expected.AnimalId, result.AnimalId);
            Assert.AreEqual(expected.Value, result.Value);
        }


        private void AssertAnimalsViewModel(AnimalViewModel expected, AnimalViewModel result)
        {
            Assert.AreEqual(expected.AnimalId, result.AnimalId);
            Assert.AreEqual(expected.CategoryId, result.CategoryId);
            Assert.AreEqual(expected.Age, result.Age);
            Assert.AreEqual(expected.CategoryName, result.CategoryName);
            Assert.AreEqual(expected.Description, result.Description);
            Assert.AreEqual(expected.ImageId, result.ImageId);
            Assert.AreEqual(expected.Name, result.Name);

            Assert.AreEqual(expected.Comments.Count(), result.Comments.Count());

            for (int i = 0; i < expected.Comments.Count(); i++)
            {
                AssertCommentsViewModel(expected.Comments.ElementAt(i), result.Comments.ElementAt(i));
            }

        }


        private void AssertCategoryViewModel(CategoryViewModel expected, CategoryViewModel result)
        {
            Assert.AreEqual(expected.CategoryId, result.CategoryId);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Animals.Count(), result.Animals.Count());
            for (int i = 0; i < expected.Animals.Count(); i++)
            {
                AssertAnimalsViewModel(expected.Animals.ElementAt(i), result.Animals.ElementAt(i));
            }
        }


        private void AssertComments(Comment expected, Comment result)
        {
            Assert.AreEqual(expected.CommentId, result.CommentId);
            Assert.AreEqual(expected.AnimalId, result.AnimalId);
            Assert.AreEqual(expected.Value, result.Value);
        }


        private void AssertAnimals(Animal expected, Animal result)
        {
            Assert.AreEqual(expected.AnimalId, result.AnimalId);
            Assert.AreEqual(expected.CategoryId, result.CategoryId);
            Assert.AreEqual(expected.Age, result.Age);
            AssertImage(expected.Image, result.Image);
            Assert.AreEqual(expected.Description, result.Description);
            Assert.AreEqual(expected.ImageId, result.ImageId);
            Assert.AreEqual(expected.Name, result.Name);

            Assert.AreEqual(expected.Comments.Count(), result.Comments.Count());

            for (int i = 0; i < expected.Comments.Count(); i++)
            {
                AssertComments(expected.Comments.ElementAt(i), result.Comments.ElementAt(i));
            }

        }

        private void AssertImage(Image expected,Image result)
        {
            //Assert.AreEqual(expected.ImageId, result.ImageId);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Data.Length, result.Data.Length);

            for (int i = 0; i < expected.Data.Length; i++)
            {
                Assert.AreEqual(expected.Data[i], result.Data[i]);
            }

        }


        private void AssertCategory(Category expected, Category result)
        {
            Assert.AreEqual(expected.CategoryId, result.CategoryId);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Animals.Count(), result.Animals.Count());
            for (int i = 0; i < expected.Animals.Count(); i++)
            {
                AssertAnimals(expected.Animals.ElementAt(i), result.Animals.ElementAt(i));
            }
        }


    }
}
