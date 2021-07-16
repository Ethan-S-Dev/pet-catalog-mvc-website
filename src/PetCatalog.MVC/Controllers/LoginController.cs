using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.MVC.ViewModels;

namespace PetCatalog.MVC.Controllers
{

    public class LoginController : Controller
    {
        private readonly IAuthService authService;
        private readonly IMapper mapper;

        public LoginController(IAuthService authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;

        }

        [HttpGet]
        public IActionResult Index([FromQuery] string path)
        {
            var userView = new LoginViewModel()
            {
                RedirectPath = path
            };
            return View("Login", userView);
        }

        [HttpPost]
        public IActionResult Authenticate(LoginViewModel userView)
        {
            var user = mapper.Map<User>(userView);

            var userWithToken = authService.Authenticate(user,userView.KeepLoggedIn);

            if (userWithToken is null)
            {
                ViewBag.Error = "Password or Email are incorrect.";
                return View("Login", userView);
            }

            if (userWithToken.RefreshToken.KeepLoggedIn)
            {
                var options = new CookieOptions();
                options.Expires = userWithToken.RefreshToken.ExpiryDate;

                this.HttpContext.Response.Cookies.Append("accessToken", userWithToken.AccessToken, options);
                this.HttpContext.Response.Cookies.Append("refreshToken", userWithToken.RefreshToken.Token, options);
            }
            else
            {             
                this.HttpContext.Session.SetString("accessToken", userWithToken.AccessToken);
                this.HttpContext.Session.SetString("refreshToken", userWithToken.RefreshToken.Token);
            }

            userView.RedirectPath ??= Url.Content("~/");

            return Redirect(userView.RedirectPath);
        }
    }
}
