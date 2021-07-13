using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
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
    public class AuthorizeMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {

        private readonly AuthorizationMiddlewareResultHandler
        DefaultHandler = new AuthorizationMiddlewareResultHandler();

        private readonly IAuthService authService;

        public AuthorizeMiddlewareResultHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {

            if (!authorizeResult.Challenged)
            {
                await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
                return;
            }

            var response = context.Response;

            string refreshToken;
            if (!context.Request.Cookies.TryGetValue("refreshToken", out refreshToken))
            {
<<<<<<< HEAD
                refreshToken = context.Session.GetString("refreshToken");
                if (refreshToken is null)
                {
                    await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
                    response.Redirect($"/login?path={context.Request.Path}");
                    return;
                }
=======
                await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
                response.Redirect($"/login?path={context.Request.Path}");
                return;
>>>>>>> 4ed9177dc3409562cd9d0939a3330decd16c467e
            }

            string accessToken;
            if (!context.Request.Cookies.TryGetValue("accessToken", out accessToken))
            {
<<<<<<< HEAD
                accessToken = context.Session.GetString("accessToken");
                if (accessToken is null)
                {
                    await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
                    response.Redirect($"/login?path={context.Request.Path}");
                    return;
                }
=======
                await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
                response.Redirect($"/login?path={context.Request.Path}");
                return;
>>>>>>> 4ed9177dc3409562cd9d0939a3330decd16c467e
            }

            if (refreshToken is null || accessToken is null)
            {
                await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
                response.Redirect($"/login?path={context.Request.Path}");
                return;
            }

            var refreshRequest = new RefreshRequest() { RefreshToken = refreshToken, AccessToken = accessToken };
            var userWithTokens = authService.RefreshToken(refreshRequest);

            if(userWithTokens is null)
            {
                await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
                response.Redirect($"/login?path={context.Request.Path}");
                return;
            }

            var options = new CookieOptions();
            options.SameSite = SameSiteMode.Strict;
            options.Expires = userWithTokens.RefreshToken.ExpiryDate;

            context.Request.Headers.Remove("Authorization");
            context.Request.Headers.Add("Authorization", "Bearer " + userWithTokens.AccessToken);
            context.Response.Cookies.Delete("accessToken");
<<<<<<< HEAD
            context.Session.Remove("accessToken");
=======
>>>>>>> 4ed9177dc3409562cd9d0939a3330decd16c467e

            if (userWithTokens.RefreshToken.KeepLoggedIn)
                context.Response.Cookies.Append("accessToken", userWithTokens.AccessToken, options);
            else
<<<<<<< HEAD
                context.Session.SetString("accessToken", userWithTokens.AccessToken);
=======
                context.Response.Cookies.Append("accessToken", userWithTokens.AccessToken);
>>>>>>> 4ed9177dc3409562cd9d0939a3330decd16c467e

            context.Response.StatusCode = (int)HttpStatusCode.OK;

            await next(context);
        }
    }

    public class Show404Requirement : IAuthorizationRequirement { }
}
