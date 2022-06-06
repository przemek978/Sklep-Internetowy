using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PROJEKT.Models;

namespace PROJEKT.Data
{
    public class CompanyPROJEKTContext : DbContext
    {
        public CompanyPROJEKTContext (DbContextOptions<CompanyPROJEKTContext> options)
            : base(options)
        {
        }

        public DbSet<PROJEKT.Models.Category> Category { get; set; }
        public DbSet<PROJEKT.Models.Product> Product { get; set; }
        public DbSet<PROJEKT.Models.ProductCategory> ProductCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            modelBuilder.Entity<ProductCategory>().HasKey(c => new { c.ProductID, c.CategoryID });
            modelBuilder.Entity<ProductCategory>().HasOne(tc => tc.Product)
                .WithMany(t => t.Categories)
                .HasForeignKey(b => b.ProductID).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductCategory>().HasOne(pc => pc.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(b => b.CategoryID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
