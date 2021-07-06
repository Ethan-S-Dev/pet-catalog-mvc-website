using Microsoft.EntityFrameworkCore;
using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.Context
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

            modelBuilder.Entity<Image>().HasData(new Image()
            {
                ImageId = 1,
                Name ="default.png"
            }); ;

        }
    }
}
