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
    public class CategoryRepositoryTests
    {
        private Mock<IConfiguration> config;
        private DbContextOptions<PetCatalogDbContext> options;
        private PetCatalogDbContext dbContext;
        private DataEntities entities;


        [TestInitialize]
        public void Init()
        {
            config = new Mock<IConfiguration>();
            config.Setup(cfg => cfg["DefaultImageName"]).Returns("imagename");
            config.Setup(cfg => cfg["DefaultImageId"]).Returns("5");
            var dboptions = new DbContextOptionsBuilder<PetCatalogDbContext>();
            dboptions.UseInMemoryDatabase("testDb");
            options = dboptions.Options;
            dbContext = new PetCatalogDbContext(dboptions.Options, config.Object);
            dbContext.Database.EnsureCreated();
            entities = new DataEntities();
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CategoryRepositoryTests_Create_CallWithCategory()
        {
            // Arrange
            var input = new Category()
            {
                Name = "test",
                CategoryId = 5,
            };

            var rep = new CategoryRepository(dbContext);

            // Act

            rep.Create(input);


            // Assert

            var result = dbContext.Categories.FirstOrDefault(cat => cat.Name == "test");

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Name);
            Assert.AreEqual(5, result.CategoryId);

        }

        [TestMethod]
        public void CategoryRepositoryTests_Delete_CallWithCategoryId_TrowException()
        {
            // Arrange
            var rep = new CategoryRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.Delete(1));
        }

        [TestMethod]
        public void CategoryRepositoryTests_Update_CallWithCategory_TrowException()
        {
            // Arrange          
            var input = new Category()
            {
                CategoryId = 45,
                Name = "test"
            };
            var rep = new CategoryRepository(dbContext);
            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.Update(input));
        }


        [TestMethod]
        public void CategoryRepositoryTests_Get_CallWithCategoryId_ReturnCategory()
        {
            // Arrange
            var input = 5;
            var category = new Category()
            {
                Name = "test",
                CategoryId = input,
            };

            var rep = new CategoryRepository(dbContext);

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            // Act

            var result = rep.Get(input);

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Name);
            Assert.AreEqual(input, result.CategoryId);

        }

        [TestMethod]
        public void CategoryRepositoryTests_GetAll_ReturnCategorys()
        {
            // Arrange
            var catelist = entities.GetCategorys().OrderByDescending(d => d.Name.ToLower());

            var rep = new CategoryRepository(dbContext);

            foreach (var ani in catelist)
            {
                dbContext.Categories.Add(ani);
            }

            dbContext.SaveChanges();

            var expect = entities.GetCategorys().OrderBy(d => d.Name.ToLower()).ToList();

            // Act

            var result = rep.GetAll();

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expect.Count(), result.Count());
            for (int i = 0; i < expect.Count(); i++)
            {
                DataEntities.AssertCategory(expect.ElementAt(i), result.ElementAt(i));
            }
        }
     
    }
}
