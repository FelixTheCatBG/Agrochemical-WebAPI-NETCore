using AgrochemicalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Data
{
    public class Initializer
    {
        public static async Task Initialize(AgrochemicalDbContext context)
        {
            //Ensures the database is deleted and created everytime we run the program
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var productCategories = new[]
            {
                new ProductCategory() {Name = "Pesticide"},
                new ProductCategory() {Name = "Insecticide"}
            };

            var products = new[]
            {
                new Product() {Name="Agrochemical100",Description="Description1",Dose = "250ml/100l", ProductCategory=productCategories[0]},
                new Product() {Name="Agrochemical101",Description="Description2",Dose = "350ml/100l", ProductCategory=productCategories[0]},
                new Product() {Name="Agrochemical102",Description="Description3",Dose = "450ml/100l", ProductCategory=productCategories[1]},
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            var packages = new[]
            {
                new Package() {Price=20,Amount = 1, Unit="l", Product=products[0]},
                new Package() {Price=20,Amount = 20, Unit="l", Product=products[1]},
                new Package() {Price=20,Amount = 30, Unit="l", Product=products[2]},
            };

            context.Packages.AddRange(packages);
            context.SaveChanges();

        }
    }
}
