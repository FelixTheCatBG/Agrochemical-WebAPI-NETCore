using System;
using System.Collections.Generic;
using System.Text;
using AgrochemicalAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgrochemicalAPI.Data
{
    public class AgrochemicalDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<CropCategory> CropCategories { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<CropProduct> CropProducts { get; set; }
        public DbSet<Illness> Illnesses { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<IllnessSymptom> IllnessSymptoms { get; set; }
        public DbSet<CropIllness> CropIllnesses { get; set; }
        public DbSet<ProductIllness> ProductIllnesses { get; set; }
        public DbSet<TokenModel> Tokens { get; set; }
        public DbSet<ProductCropIllness> ProductCropIllnesses { get; set; }

        public AgrochemicalDbContext(DbContextOptions<AgrochemicalDbContext> options)
            : base(options)
        {
        }
          
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                  new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                  new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
                  //new { Id = "3", Name = "Moderator", NormalizedName = "MODERATOR" }
            );

            //ProductCategory - Products One-to-Many
            builder.Entity<ProductCategory>()
                .HasMany(pc => pc.Products)
                .WithOne(p => p.ProductCategory)
                .HasForeignKey(p => p.ProductCategoryId);

            //Product - Packages  One-to-Many
            builder.Entity<Product>()
                .HasMany(pr => pr.Packages)
                .WithOne(pa => pa.Product)
                .HasForeignKey(pa => pa.ProductId);

            //ProductCategory - Products One-to-Many
            builder.Entity<CropCategory>()
                .HasMany(cc => cc.Crops)
                .WithOne(c => c.CropCategory)
                .HasForeignKey(c => c.CropCategoryId);

            //Crop - Product Many-to-Many with composite key
            builder.Entity<CropProduct>()
                .HasKey(cp => new { cp.CropId, cp.ProductId });

            builder.Entity<Crop>()
                .HasMany(c => c.CropProducts)
                .WithOne(cp => cp.Crop)
                .HasForeignKey(cp => cp.CropId);

            builder.Entity<Product>()
                .HasMany(p => p.CropProducts)
                .WithOne(cp => cp.Product)
                .HasForeignKey(cp => cp.ProductId);

            //Crop - Illness Many-to-Many with composite key
            builder.Entity<CropIllness>()
                .HasKey(ci => new { ci.CropId, ci.IllnessId });

            builder.Entity<Crop>()
                .HasMany(c => c.CropIllnesses)
                .WithOne(ci => ci.Crop)
                .HasForeignKey(ci => ci.CropId);

            builder.Entity<Illness>()
                .HasMany(p => p.CropIllnesses)
                .WithOne(ci => ci.Illness)
                .HasForeignKey(ci => ci.IllnessId);

            //Product - Illness Many-to-Many with composite key
            builder.Entity<ProductIllness>()
                .HasKey(ci => new { ci.ProductId, ci.IllnessId });

            builder.Entity<Product>()
                .HasMany(c => c.ProductIllnesses)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId);

            builder.Entity<Illness>()
                .HasMany(p => p.ProductIllnesses)
                .WithOne(pi => pi.Illness)
                .HasForeignKey(pi => pi.IllnessId);

            //Illness - Symptom Many-to-Many with composite key
            builder.Entity<IllnessSymptom>()
                .HasKey(ils => new { ils.IllnessId, ils.SymptomId });

            builder.Entity<Illness>()
                .HasMany(i => i.IllnessSymptoms)
                .WithOne(ils => ils.Illness)
                .HasForeignKey(ils => ils.IllnessId);

            builder.Entity<Symptom>()
                .HasMany(s => s.IllnessSymptoms)
                .WithOne(ils => ils.Symptom)
                .HasForeignKey(ils => ils.SymptomId);

            builder.Entity<ProductCropIllness>()
            .HasKey(e => new { e.ProductCropIllnessId });

            builder.Entity<Product>()
                .HasMany(p => p.ProductCropIllnesses)
                .WithOne(ils => ils.Product)
                .HasForeignKey(ils => ils.ProductId);

            builder.Entity<Crop>()
                .HasMany(s => s.ProductCropIllnesses)
                .WithOne(ils => ils.Crop)
                .HasForeignKey(ils => ils.CropId);

            builder.Entity<Illness>()
                .HasMany(s => s.ProductCropIllnesses)
                .WithOne(ils => ils.Illness)
                .HasForeignKey(ils => ils.IllnessId);

        }
    }
}
