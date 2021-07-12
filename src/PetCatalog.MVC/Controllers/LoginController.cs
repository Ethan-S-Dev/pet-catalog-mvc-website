using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetCatalog.Application.Auth;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetCatalog.MVC.Controllers
{

    public class LoginController : Controller
    {
        private readonly IAuthService authService;
        private readonly IConfiguration configuration;

        public LoginController(IAuthService authService,IConfiguration configuration)
        {
            this.authService = authService;
            this.configuration = configuration;
        }

        // GET: <LoginController>
        [HttpGet]
        public IActionResult Index([FromQuery] string path)
        {
            ViewBag.RedirectPath = path;
            return View("Login");
        }


        [HttpPost]
        public IActionResult Authenticate(User user,[FromQuery] string path)
        {
            var userWithToken = authService.Authenticate(user);

            if (userWithToken is null)
                return View("Login", user);


            var options = new CookieOptions();
            options.Expires = DateTime.UtcNow.AddMonths(configuration.GetSection("JWTSettings").GetValue<int>("RefreshExpiresIn"));

            this.HttpContext.Response.Cookies.Append("accessToken", userWithToken.AccessToken, options);
            this.HttpContext.Response.Cookies.Append("refreshToken", userWithToken.RefreshToken, options);

            path ??= Url.Content("~/");

            return Redirect(path);
        }


    }
}
