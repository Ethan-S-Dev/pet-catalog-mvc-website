using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Middlewares
{
    public class AuthHeaderMiddleware
    {
        private readonly RequestDelegate next;

        public AuthHeaderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string token;
            if (!context.Request.Cookies.TryGetValue("accessToken", out token))
                token = context.Session.GetString("accessToken");

            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Remove("Authorization");
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }

            await next(context);
        }
    }
}
