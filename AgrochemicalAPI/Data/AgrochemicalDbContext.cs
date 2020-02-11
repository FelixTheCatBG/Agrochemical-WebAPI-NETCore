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
        public DbSet<Crop> Crops { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Manufacturerr> Manufacturerr { get; set; }
        public DbSet<DiseaseSymptom> DiseaseSymptoms { get; set; }
        public DbSet<ProductAdvantage> ProductAdvantages { get; set; }
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
            );

            //ProductCategory - Products One-to-Many
            builder.Entity<ProductCategory>()
                .HasMany(pc => pc.Products)
                .WithOne(p => p.ProductCategory)
                .HasForeignKey(p => p.ProductCategoryId);

            //Product - Packages  One-to-Many
            builder.Entity<Product>()
                .HasMany(pr => pr.Packages)
                .WithOne(pac => pac.Product)
                .HasForeignKey(pac => pac.ProductId);

            //Product - ProductAdvantages  One-to-Many
            builder.Entity<Product>()
            .HasMany(pr => pr.ProductAdvantages)
            .WithOne(pa => pa.Product)
            .HasForeignKey(pa => pa.ProductId);

            //Product - Manufacturerr  One-to-Many
            builder.Entity<Manufacturerr>()
            .HasMany(pr => pr.Products)
            .WithOne(pa => pa.Manufacturerr)
            .HasForeignKey(pa => pa.ManufacturerrId);


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

            //ProductCrop Disease
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
