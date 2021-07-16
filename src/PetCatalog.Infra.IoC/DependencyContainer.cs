using Microsoft.Extensions.DependencyInjection;
using PetCatalog.Application.Interfaces;
using PetCatalog.Application.Services;
using PetCatalog.Domain.Interfaces;
using PetCatalog.Infra.Data.Contexts;
using PetCatalog.Infra.Data.Interfaces;
using PetCatalog.Infra.Data.Repositorys;

namespace PetCatalog.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            // PetShop.Application
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IAuthService, AuthorizationService>();

            // PetShop.Domain.Interfaces | PetShop.Infra.Data.Repositorys
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            // PetShop.Infra.Data.Interfaces | PetShop.Infra.Data.Repositorys
            services.AddScoped<IFileContext, ImageFileContext>();
        }

    }
}
