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
    public class CommentRepositoryTests
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
        }

        [TestMethod]
        public void CommentRepositoryTests_Create_CallWithComment()
        {
            // Arrange
            var input = new Comment()
            {
                CommentId = 5,
                AnimalId = 2,
                Value = "test"
            };

            var rep = new CommentRepository(dbContext);

            // Act

            rep.Create(input);


            // Assert

            var result = dbContext.Comments.FirstOrDefault(ani => ani.Value == "test");

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Value);
            Assert.AreEqual(2, result.AnimalId);
            Assert.AreEqual(5, result.CommentId);
        }

        [TestMethod]
        public void CommentRepositoryTests_Delete_CallWithCommentId_ReturnComment()
        {
            // Arrange
            var input = 5;
            var comment = new Comment()
            {
                CommentId = input,
                Value = "test",
                AnimalId = 2
            };

            var rep = new CommentRepository(dbContext);

            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();

            // Act

            var result = rep.Delete(input);

            // Assert

            var novalue = dbContext.Comments.Find(input);

            Assert.IsNull(novalue);
            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Value);
            Assert.AreEqual(2, result.AnimalId);
            Assert.AreEqual(input, result.CommentId);
        }

        [TestMethod]
        public void CommentRepositoryTests_Update_CallWithComment()
        {
            // Arrange
            var rep = new CommentRepository(dbContext);
            var input = new Comment()
            {
                AnimalId = 1,
                CommentId = 4,
                Value = "test"
            };
            // Act
            // Assert

            Assert.ThrowsException<NotImplementedException>(() => rep.Update(input));
        }


        [TestMethod]
        public void CommentRepositoryTests_Get_CallWithCommentId_ReturnComment()
        {
            // Arrange
            var input = 5;
            var comment = new Comment()
            {
                CommentId = input,
                Value = "test",
                AnimalId = 2,
            };

            var rep = new CommentRepository(dbContext);

            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();

            // Act

            var result = rep.Get(input);

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Value);
            Assert.AreEqual(2, result.AnimalId);
            Assert.AreEqual(input, result.CommentId);
        }

        [TestMethod]
        public void CommentRepositoryTests_GetAll_ReturnComments()
        {
            // Arrange
            var commentlsit = entities.GetComments().OrderBy(c=>c.CommentId);

            var rep = new CommentRepository(dbContext);

            foreach (var ani in commentlsit)
            {
                dbContext.Comments.Add(ani);
            }

            dbContext.SaveChanges();

            var expect = entities.GetComments().ToList();

            // Act

            var result = rep.GetAll();

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expect.Count(), result.Count());

        }

        [TestMethod]
        public void CommentRepositoryTests_GetAnimalComments_CallWithAnimalId_ReturnTwoComments()
        {
            // Arrange
            var input = 2;

            var comments = entities.GetComments();

            var rep = new CommentRepository(dbContext);

            foreach (var ani in comments)
            {
                dbContext.Comments.Add(ani);
            }

            dbContext.SaveChanges();

            var expect = entities.GetComments().Where(ani => ani.AnimalId == 2).ToList();

            // Act

            var result = rep.GetAnimalComments(input);

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expect.Count(), result.Count());
            for (int i = 0; i < expect.Count(); i++)
            {
                DataEntities.AssertComments(expect.ElementAt(i), result.ElementAt(i));
            }
        }

        [TestMethod]
        public void CommentRepositoryTests_DeleteAnimalComments_CallWithAnimalId_ReturnTwoComments()
        {
            // Arrange
            var input = 2;

            var comments = entities.GetComments();

            var rep = new CommentRepository(dbContext);

            foreach (var ani in comments)
            {
                dbContext.Comments.Add(ani);
            }

            dbContext.SaveChanges();

            var expect = entities.GetComments().Where(ani => ani.AnimalId == 2).ToList();
            var left = entities.GetComments().Where(ani => ani.AnimalId != 2).Count();

            // Act

            var result = rep.DeleteAnimalComments(input);

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expect.Count(), result.Count());
            for (int i = 0; i < expect.Count(); i++)
            {
                DataEntities.AssertComments(expect.ElementAt(i), result.ElementAt(i));
            }

            Assert.AreEqual(left, dbContext.Comments.Count());
        }
    }
}
