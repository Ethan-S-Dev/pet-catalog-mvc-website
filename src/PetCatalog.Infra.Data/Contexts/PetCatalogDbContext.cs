using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetCatalog.Domain.Models;
using System;

namespace PetCatalog.Infra.Data.Contexts
{
    public class PetCatalogDbContext : DbContext
    {
        private readonly string defaultImage;
        private readonly int defaultImageId;
        public int DefaultImageId => defaultImageId;
        public PetCatalogDbContext(DbContextOptions<PetCatalogDbContext> options,IConfiguration configuration) : base(options)
        {
            this.defaultImage = configuration["DefaultImageName"];
            defaultImageId = Convert.ToInt32(configuration["DefaultImageId"]);
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().Ignore(e => e.Data);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique(true);

            modelBuilder.Entity<Image>().HasData(new Image()
            {
                ImageId = defaultImageId,
                Name = defaultImage
            }); ;

            modelBuilder.Entity<User>().HasData(new User()
            {
                UserId = 1,
                Name = "Admin",
                Email = "Admin@Email.com",
<<<<<<< HEAD
                Password = "123"
=======
                Password = "6RVUXqGTj3wmjeBYeZXwk4Eu"
>>>>>>> 4ed9177dc3409562cd9d0939a3330decd16c467e
            }); ;

        }
    }
}
