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
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<DiseaseSymptom> DiseaseSymptoms { get; set; }
        public DbSet<CropDisease> CropDiseases { get; set; }
        public DbSet<ProductDisease> ProductDiseases { get; set; }
        public DbSet<TokenModel> Tokens { get; set; }
        public DbSet<ProductCropDisease> ProductCropDiseases { get; set; }

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

            //Crop - Disease Many-to-Many with composite key
            builder.Entity<CropDisease>()
                .HasKey(ci => new { ci.CropId, ci.DiseaseId });

            builder.Entity<Crop>()
                .HasMany(c => c.CropDiseases)
                .WithOne(ci => ci.Crop)
                .HasForeignKey(ci => ci.CropId);

            builder.Entity<Disease>()
                .HasMany(p => p.CropDiseases)
                .WithOne(ci => ci.Disease)
                .HasForeignKey(ci => ci.DiseaseId);

            //Product - Disease Many-to-Many with composite key
            builder.Entity<ProductDisease>()
                .HasKey(ci => new { ci.ProductId, ci.DiseaseId });

            builder.Entity<Product>()
                .HasMany(c => c.ProductDiseases)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId);

            builder.Entity<Disease>()
                .HasMany(p => p.ProductDiseases)
                .WithOne(pi => pi.Disease)
                .HasForeignKey(pi => pi.DiseaseId);

            //Disease - Symptom Many-to-Many with composite key
            builder.Entity<DiseaseSymptom>()
                .HasKey(ils => new { ils.DiseaseId, ils.SymptomId });

            builder.Entity<Disease>()
                .HasMany(i => i.DiseaseSymtpoms)
                .WithOne(ils => ils.Disease)
                .HasForeignKey(ils => ils.DiseaseId);

            builder.Entity<Symptom>()
                .HasMany(s => s.DiseaseSymptoms)
                .WithOne(ils => ils.Symptom)
                .HasForeignKey(ils => ils.SymptomId);

            builder.Entity<ProductCropDisease>()
            .HasKey(e => new { e.ProductCropDiseaseId });

            builder.Entity<Product>()
                .HasMany(p => p.ProductCropDiseases)
                .WithOne(ils => ils.Product)
                .HasForeignKey(ils => ils.ProductId);

            builder.Entity<Crop>()
                .HasMany(s => s.ProductCropDiseases)
                .WithOne(ils => ils.Crop)
                .HasForeignKey(ils => ils.CropId);

            builder.Entity<Disease>()
                .HasMany(s => s.ProductCropDiseases)
                .WithOne(ils => ils.Disease)
                .HasForeignKey(ils => ils.DiseaseId);

        }
    }
}
