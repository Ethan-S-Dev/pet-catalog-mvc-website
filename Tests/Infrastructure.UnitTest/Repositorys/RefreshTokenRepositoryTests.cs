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
    public class RefreshTokenRepositoryTests
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
        public void RefreshTokenRepositoryTests_Create_CallWithRefreshToken()
        {
            // Arrange
            var input = new RefreshToken()
            {
                ExpiryDate = new DateTime(2000, 1, 1),
                Token = "test",
                TokenId = 1,
                UserId = 5
            };

            var rep = new RefreshTokenRepository(dbContext);

            // Act

            rep.Create(input);


            // Assert

            var result = dbContext.RefreshTokens.FirstOrDefault(reft => reft.Token == "test");

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Token);
            Assert.AreEqual(5, result.UserId);
            Assert.AreEqual(1, result.TokenId);
            Assert.AreEqual(new DateTime(2000, 1, 1), result.ExpiryDate);
        }

        [TestMethod]
        public void RefreshTokenRepositoryTests_Delete_CallWithRefreshTokenId_ThrowException()
        {
            // Arrange
            var input = 1;
            var rep = new RefreshTokenRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.Delete(input));
        }

        [TestMethod]
        public void RefreshTokenRepositoryTests_Update_CallWithRefreshToken_ThrowException()
        {
            // Arrange
            var input = new RefreshToken()
            {
                ExpiryDate = new DateTime(2000, 1, 1),
                Token = "test",
                TokenId = 1,
                UserId = 5
            };
            var rep = new RefreshTokenRepository(dbContext);


            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.Update(input));
        }


        [TestMethod]
        public void RefreshTokenRepositoryTests_Get_CallWithRefreshTokenId_ThrowException()
        {
            // Arrange
            var input = 1;
            var rep = new RefreshTokenRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.Get(input));
        }

        [TestMethod]
        public void RefreshTokenRepositoryTests_GetAll_ThrowException()
        {
            // Arrange
            var rep = new RefreshTokenRepository(dbContext);

            // Act
            // Assert
            Assert.ThrowsException<NotImplementedException>(() => rep.GetAll());
        }

        [TestMethod]
        public void RefreshTokenRepositoryTests_GetRecentTokens_CallWithToken_ReturnRefrehTokens()
        {
            // Arrange


            var tokenlist = new List<RefreshToken>()
            {
                new RefreshToken()
            {
                ExpiryDate = new DateTime(2000, 1, 1),
                Token = "test",
                TokenId = 1,
                UserId = 1
            },
                new RefreshToken()
            {
                ExpiryDate = new DateTime(2001, 1, 1),
                Token = "test",
                TokenId = 2,
                UserId = 2
            },
                new RefreshToken()
            {
                ExpiryDate = new DateTime(2002, 1, 1),
                Token = "test",
                TokenId = 3,
                UserId = 3
            }



        };

            var rep = new RefreshTokenRepository(dbContext);

            foreach (var token in tokenlist)
            {
                dbContext.RefreshTokens.Add(token);
            }

            dbContext.SaveChanges();

            var expectation = tokenlist.Where(t => t.Token == "test").OrderByDescending(rt => rt.ExpiryDate);

            // Act

            var result = rep.GetRecentTokens("test");

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expectation.Count(), result.Count());
            for (int i = 0; i < expectation.Count(); i++)
            {
                AssertRefreshTokens(expectation.ElementAt(i), result.ElementAt(i));
            }
        }

        [TestMethod]
        public void RefreshTokenRepositoryTests_DeleteUserRecentTokens_CallWithUserAndToken()
        {
            // Arrange
            var tokenlist = new List<RefreshToken>()
            {
                new RefreshToken()
            {
                ExpiryDate = new DateTime(2000, 1, 1),
                Token = "test",
                TokenId = 4,
                UserId = 2
            },
                new RefreshToken()
            {
                ExpiryDate = new DateTime(2001, 1, 1),
                Token = "test2",
                TokenId = 5,
                UserId = 2
            },
                new RefreshToken()
            {
                ExpiryDate = new DateTime(2002, 1, 1),
                Token = "test2",
                TokenId = 3,
                UserId = 2
            }

            };

            var user = new User()
            {
                UserId = 2
            };

            

            var rep = new RefreshTokenRepository(dbContext);
            
            dbContext.Users.Add(user);

            foreach (var token in tokenlist)
            {
                dbContext.RefreshTokens.Add(token);
            }


            dbContext.SaveChanges();

            // Act

            rep.DeleteUserToken(user, "test2");

            // Assert

            var result = dbContext.RefreshTokens.Where(t => t.UserId == 2).ToList();
            var left = result.First();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(new DateTime(2000, 1, 1), left.ExpiryDate);
            Assert.AreEqual("test", left.Token);
            Assert.AreEqual(4, left.TokenId);
            Assert.AreEqual(2, left.UserId);
        }

        [TestMethod]
        public void RefreshTokenRepositoryTests_DeleteAllUserTokens_CallWithUser()
        {
            // Arrange
            var tokenlist = new List<RefreshToken>()
            {
                new RefreshToken()
            {
                ExpiryDate = new DateTime(2000, 1, 1),
                Token = "test",
                TokenId = 1,
                UserId = 4
            },
                new RefreshToken()
            {
                ExpiryDate = new DateTime(2001, 1, 1),
                Token = "test2",
                TokenId = 2,
                UserId = 4
            },
                new RefreshToken()
            {
                ExpiryDate = new DateTime(2002, 1, 1),
                Token = "test2",
                TokenId = 3,
                UserId = 4
            },

            new RefreshToken()
            {
                ExpiryDate = new DateTime(2003, 1, 1),
                Token = "test3",
                TokenId = 4,
                UserId = 2
            }

            };

            var user = new User()
            {
                UserId = 4
            };



            var rep = new RefreshTokenRepository(dbContext);

            foreach (var token in tokenlist)
            {
                dbContext.RefreshTokens.Add(token);
            }

            dbContext.Users.Add(user);

            dbContext.SaveChanges();

            // Act

            rep.DeleteAllUserTokens(user);

            // Assert

            var result = dbContext.RefreshTokens.Where(t => t.UserId == 4).ToList();
            var left = dbContext.RefreshTokens.First();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
            Assert.AreEqual(new DateTime(2003, 1, 1), left.ExpiryDate);
            Assert.AreEqual("test3", left.Token);
            Assert.AreEqual(4, left.TokenId);
            Assert.AreEqual(2, left.UserId);
        }

        private void AssertRefreshTokens(RefreshToken expect,RefreshToken result)
        {
            Assert.AreEqual(expect.TokenId, result.TokenId);
            Assert.AreEqual(expect.UserId, result.UserId);
            Assert.AreEqual(expect.Token, result.Token);
            Assert.AreEqual(expect.ExpiryDate, result.ExpiryDate);
        }
    }
}
