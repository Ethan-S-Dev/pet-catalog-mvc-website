using Microsoft.EntityFrameworkCore;
using PetCatalog.Domain.Models;

namespace PetCatalog.Infra.Data.Contexts
{
    public class PetCatalogDbContext : DbContext
    {
        public PetCatalogDbContext(DbContextOptions<PetCatalogDbContext> options) : base(options)
        {

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
                ImageId = 1,
                Name ="default.png"
            }); ;

        }
    }
}
