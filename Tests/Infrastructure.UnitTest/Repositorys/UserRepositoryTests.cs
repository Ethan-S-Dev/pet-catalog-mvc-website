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
    public class UserRepositoryTests
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
        public void UserRepositoryTests_Create_CallWithUser_ThrowException()
        {
            // Arrange
            var input = new User
            {
                Email = "email@email.com",
                UserId = 1,
                Name = "user",
                Password = "testpass"
            };

            var rep = new UserRepository(dbContext);


            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.Create(input));

        }

        [TestMethod]
        public void UserRepositoryTests_Delete_CallWithAnimalId_ThrowException()
        {
            // Arrange
            var input = 5;
            var rep = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.Delete(input));
        }

        [TestMethod]
        public void UserRepositoryTests_Update_CallWithUser_ThrowException()
        {
            // Arrange
            var input = new User
            {
                Email = "email@email.com",
                UserId = 1,
                Name = "user",
                Password = "testpass"
            };

            var rep = new UserRepository(dbContext);


            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.Update(input));
        }


        [TestMethod]
        public void UserRepositoryTests_Get_CallWithUserId_ThrowException()
        {
            // Arrange
            var input = 5;
            var rep = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.Get(input));
        }

        [TestMethod]
        public void UserRepositoryTests_GetAll_ThrowException()
        {
            // Arrange
            var rep = new UserRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.GetAll());
        }

        [TestMethod]
        public void UserRepositoryTests_Get_CallWithUser_ReturnUser()
        {
            // Arrange
            var input = new User
            {
                Email = "email@email.com",
                Password = "testpass"
            };

            var expected = new User
            {
                Email = "email@email.com",
                UserId = 2,
                Name = "user",
                Password = "testpass"
            };

            var rep = new UserRepository(dbContext);

            dbContext.Users.Add(expected);

            dbContext.SaveChanges();

            // Act

            var result = rep.Get(input);

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Email, result.Email);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Password, result.Password);
            Assert.AreEqual(expected.UserId, result.UserId);
        }

        [TestMethod]
        public void UserRepositoryTests_Get_CallWithEmail_ReturnUser()
        {
            // Arrange
            var input = "email@email.com";

            var expected = new User
            {
                Email = "email@email.com",
                UserId = 4,
                Name = "user",
                Password = "testpass"
            };

            var rep = new UserRepository(dbContext);

            dbContext.Users.Add(expected);

            dbContext.SaveChanges();

            // Act

            var result = rep.Get(input);

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Email, result.Email);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Password, result.Password);
            Assert.AreEqual(expected.UserId, result.UserId);
        }
    }

}
