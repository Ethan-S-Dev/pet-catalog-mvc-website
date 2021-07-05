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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { CategoryId = 1, Name = "Birds"},
                new Category() { CategoryId = 2, Name = "Snakes" },
                new Category() { CategoryId = 3, Name = "Dogs" },
                new Category() { CategoryId = 4, Name = "Cats" }
            );
        }
    }
}
