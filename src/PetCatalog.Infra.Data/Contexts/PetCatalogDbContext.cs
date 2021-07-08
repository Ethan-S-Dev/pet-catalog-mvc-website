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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().Ignore(e => e.Data);

            modelBuilder.Entity<Image>().HasData(new Image()
            {
                ImageId = defaultImageId,
                Name = defaultImage
            }); ;

        }
    }
}
