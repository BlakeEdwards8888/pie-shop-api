using Microsoft.EntityFrameworkCore;
using PieShop.API.Entities;
using PieShop.API.Models;

namespace PieShop.API.DbContexts { 
public class PieShopContext : DbContext
    {
        public DbSet<Pie> Pies { get; set; }

        public PieShopContext(DbContextOptions<PieShopContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pie>().HasData(
                new Pie()
                {
                    Id = 1,
                    Name = "Apple Pie",
                    Description = "Our famous apple pie",
                    Price = 12.95,
                    Category = "fruit-pie"
                },
                new Pie()
                {
                    Id = 2,
                    Name = "Blueberry Pie",
                    Description = "A delicious pie filled with succulent blueberries",
                    Price = 12.95,
                    Category = "fruit-pie"
                },
                new Pie()
                {
                    Id = 3,
                    Name = "Cheesecake",
                    Description = "A decadent, creamy cheesecake",
                    Price = 14.95,
                    Category = "cheesecake"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
