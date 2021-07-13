using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetCatalog.Application.Auth;
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
    public class AuthorizationServiceTests
    {
        private JWTSettings jWTSettings;
        private Mock<IOptions<JWTSettings>> ops;
        private Mock<IUserRepository> userRepo;
        private Mock<IRefreshTokenRepository> refRepo;

        [TestInitialize]
        public void Init()
        {
            jWTSettings = new JWTSettings()
            {
                JwtExpiresIn = 5,
                RefreshExpiresIn = 5,
                SecretKey = "this is a secret"
            };
            ops = new Mock<IOptions<JWTSettings>>();
            userRepo = new Mock<IUserRepository>();
            refRepo = new Mock<IRefreshTokenRepository>();
        }

        [TestMethod]
        public void AuthorizationServiceTests_Authenticate_CallWithUser_ReturenUserWithToken()
        {
            ops.SetupGet(d => d.Value).Returns(jWTSettings);
            // Arrange
            var input = new User()
            {
                Email = "test@test.com",
                Password = "flop",
            };

            userRepo.Setup(rep => rep.Get(It.IsAny<User>())).Returns(new User()
            {
                Name = "test",
                Email = "test@test.com",
                Password = "flop",
                UserId = 1,
                RefreshTokens = new List<RefreshToken>() { }
            });

            refRepo.Setup(rep => rep.Create(It.IsAny<RefreshToken>()));
            
            var authService = new AuthorizationService(ops.Object, userRepo.Object, refRepo.Object);
            // Act

            var result = authService.Authenticate(input);

            // Assert

            Assert.IsNotNull(result.AccessToken);
            Assert.IsNotNull(result.RefreshToken);
            Assert.IsNotNull(result.RefreshToken.Token);

        }

        [TestMethod]
        public void AuthorizationServiceTests_GetEmptyUser_ReturenUser()
        {
            var authService = new AuthorizationService(ops.Object, userRepo.Object, refRepo.Object);
            // Act

            var result = authService.GetEmptyUser();

            // Assert

            Assert.IsNotNull(result);
            Assert.IsNull(result.Name);
            Assert.IsNull(result.Password);
            Assert.IsNull(result.Email);
            Assert.IsNull(result.RefreshTokens);
            Assert.AreEqual(0, result.UserId);

        }
    }
}
