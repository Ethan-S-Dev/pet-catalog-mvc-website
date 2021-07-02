using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Infra.Data.Context
{
    public class PetShopDbContext : DbContext
    {
        public PetShopDbContext(DbContextOptions<PetShopDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // TODO: Add preexisting data to the database
        }
    }
}
