using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCatalog.Domain.Models;
using PetCatalog.Infra.Data.Contexts;
using PetCatalog.Infra.Data.Repositorys;
using Shared.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitTest.Repositorys
{
    [TestClass]
    public class AnimalRepositoryTests
    {
        private PetCatalogDbContext dbContext;
        private Mock<IConfiguration> config;
        private DataEntities entities;

        [TestInitialize]
        public void Init()
        {
            config = new Mock<IConfiguration>();
            config.Setup(cfg => cfg["DefaultImageName"]).Returns("imagename");
            config.Setup(cfg => cfg["DefaultImageId"]).Returns("5");
            var dboptions = new DbContextOptionsBuilder<PetCatalogDbContext>();
            dboptions.UseInMemoryDatabase("testDb");

            dbContext = new PetCatalogDbContext(dboptions.Options, config.Object);
            dbContext.Database.EnsureCreated();
            entities = new DataEntities();
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [TestMethod]
        public void AnimalRepositoryTests_Create_CallWithAnimal()
        {
            // Arrange
            var input = new Animal()
            {
                Name = "test",
                Age = 2,
                CategoryId = 1,
                Description = "test des"
            };

            var rep = new AnimalRepository(dbContext);

            // Act

            rep.Create(input);


            // Assert

            var result = dbContext.Animals.FirstOrDefault(ani => ani.Name == "test");

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Name);
            Assert.AreEqual(2, result.Age);
            Assert.AreEqual(1, result.CategoryId);
            Assert.AreEqual("test des", result.Description);
        }

        [TestMethod]
        public void AnimalRepositoryTests_Delete_CallWithAnimalId_ReturnAnimal()
        {
            // Arrange
            var input = 5;
            var animal = new Animal()
            {
                AnimalId = input,
                Name = "test",
                Age = 2,
                CategoryId = 1,
                Description = "test des"
            };

            var rep = new AnimalRepository(dbContext);

            dbContext.Animals.Add(animal);
            dbContext.SaveChanges();

            // Act

            var result = rep.Delete(input);

            // Assert

            var noanimal = dbContext.Animals.Find(input);

            Assert.IsNull(noanimal);
            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Name);
            Assert.AreEqual(2, result.Age);
            Assert.AreEqual(1, result.CategoryId);
            Assert.AreEqual("test des", result.Description);
            Assert.AreEqual(input, result.AnimalId);
        }

        [TestMethod]
        public void AnimalRepositoryTests_Update_CallWithAnimal()
        {
            // Arrange
            var id = 6;
            var input = new Animal()
            {
                AnimalId = id,
                Name = "test",
                Age = 2,
                CategoryId = 1,
                Description = "test des"
            };
            var animal = new Animal()
            {
                AnimalId = id,
                Name = "before",
                Age = 6,
                CategoryId = 7,
                Description = "test des 3"
            };

            var rep = new AnimalRepository(dbContext);

            dbContext.Animals.Add(animal);
            dbContext.SaveChanges();

            // Act

            rep.Update(input);

            // Assert

            var result = dbContext.Animals.Find(id);

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Name);
            Assert.AreEqual(2, result.Age);
            Assert.AreEqual(1, result.CategoryId);
            Assert.AreEqual("test des", result.Description);
            Assert.AreEqual(id, result.AnimalId);
        }


        [TestMethod]
        public void AnimalRepositoryTests_Get_CallWithAnimalId_ReturnAnimal()
        {
            // Arrange
            var input = 5;
            var animal = new Animal()
            {
                AnimalId = input,
                Name = "test",
                Age = 2,
                CategoryId = 1,
                Description = "test des"
            };

            var rep = new AnimalRepository(dbContext);

            dbContext.Animals.Add(animal);
            dbContext.SaveChanges();

            // Act

            var result = rep.Get(input);

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Name);
            Assert.AreEqual(2, result.Age);
            Assert.AreEqual(1, result.CategoryId);
            Assert.AreEqual("test des", result.Description);
            Assert.AreEqual(input, result.AnimalId);
        }

        [TestMethod]
        public void AnimalRepositoryTests_GetAll_ReturnAnimals()
        {
            // Arrange
            var animallsit = entities.GetAnimals().OrderByDescending(d => d.Name.ToLower());

            var rep = new AnimalRepository(dbContext);

            foreach (var ani in animallsit)
            {
                dbContext.Animals.Add(ani);
            }
            
            dbContext.SaveChanges();

            var expectation = entities.GetAnimals().OrderBy(d => d.Name.ToLower()).ToList();

            // Act

            var result = rep.GetAll();

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expectation.Count(), result.Count());
            for (int i = 0; i < expectation.Count(); i++)
            {
                DataEntities.AssertAnimals(expectation.ElementAt(i), result.ElementAt(i));
            }
        }

        [TestMethod]
        public void AnimalRepositoryTests_GetTopCommented_ReturnTwoAnimals()
        {
            // Arrange
            var comments = entities.GetComments();
            foreach (var item in comments)
            {
                switch (item.CommentId)
                {
                    case 1:
                        item.AnimalId = 3;
                        break;
                    case 2:
                        item.AnimalId = 3;
                        break;
                    case 3:
                        item.AnimalId = 2;
                        break;
                    default:
                        break;
                }
            }

            var animallsit = entities.GetAnimals();

            var rep = new AnimalRepository(dbContext);

            foreach (var ani in animallsit)
            {
                dbContext.Animals.Add(ani);
            }

            dbContext.SaveChanges();

            var expectation = entities.GetAnimals().OrderByDescending(a => a.Comments.Count()).Take(2);

            // Act

            var result = rep.GetTopCommented();

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expectation.Count(), result.Count());
            for (int i = 0; i < expectation.Count(); i++)
            {
                DataEntities.AssertAnimals(expectation.ElementAt(i), result.ElementAt(i));
            }
        }

    }
}
