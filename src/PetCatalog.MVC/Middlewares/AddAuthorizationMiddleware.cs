using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using PetCatalog.Application.Auth;
using PetCatalog.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Middlewares
{
    public class AddAuthorizationMiddleware
    {
        private readonly RequestDelegate next;

        public AddAuthorizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string token;
            if (context.Request.Cookies.TryGetValue("accessToken", out token))
            {
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Remove("Authorization");
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
            }

            await next(context);
        }
    }
}
