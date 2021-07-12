using Microsoft.AspNetCore.Builder;
using PetCatalog.MVC.Middlewares;

namespace PetCatalog.MVC.Extensions
{
    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAddAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AddAuthorizationMiddleware>();
        }
    }
}
