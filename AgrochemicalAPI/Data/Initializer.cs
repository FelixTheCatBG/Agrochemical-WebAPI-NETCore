using AgrochemicalAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Data
{
    public class Initializer
    {
        public static async Task Initialize
            (
            AgrochemicalDbContext context,
            UserManager<ApplicationUser> _userManager
            )
        {
            //Ensures the database is deleted and created everytime we run the program
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //Seed with admin and first user
            if (await _userManager.FindByNameAsync("admin@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(user, "333444555");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

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
                new Product() {Name="Agrochemical103",Description="Description1",Dose = "250ml/100l", ProductCategory=productCategories[0]},
                new Product() {Name="Agrochemical104",Description="Description1",Dose = "250ml/100l", ProductCategory=productCategories[0]},
                new Product() {Name="Agrochemical105",Description="Description1",Dose = "250ml/100l", ProductCategory=productCategories[1]},
                new Product() {Name="Agrochemical106",Description="Description1",Dose = "250ml/100l", ProductCategory=productCategories[1]},
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            var packages = new[]
            {
                new Package() {Price=20,Amount = 1, Unit="l", Product=products[0]},
                new Package() {Price=27,Amount = 30, Unit="l", Product=products[0]},
                new Package() {Price=20,Amount = 20, Unit="l", Product=products[1]},
                new Package() {Price=20,Amount = 30, Unit="l", Product=products[2]},
            };
            context.Packages.AddRange(packages);
            context.SaveChanges();

            var cropCategories = new[]
            {
                new CropCategory() {Name="Corns"},
                new CropCategory() {Name="Trees"},
                new CropCategory() {Name="Corn"},

            };
            context.CropCategories.AddRange(cropCategories);
            context.SaveChanges();


            //Seeding with Crops
            var crops = new[]
            {
                 new Crop() {Name = "Tomatoes", CropCategory=cropCategories[0] },
                 new Crop() {Name = "Corn", CropCategory=cropCategories[0] },
                 new Crop() {Name = "Peppers", CropCategory=cropCategories[1] }, 
                 new Crop() {Name = "Apples", CropCategory=cropCategories[2] }
            };
            context.Crops.AddRange(crops);
            context.SaveChanges();

            //Seeding with Illnesses
            var illnesses = new[]
            {
                 new Illness() {Name = "WhiteIllness", Description="Description1" },
                 new Illness() {Name = "YellowIllness", Description="Description2" },
                 new Illness() {Name = "RedIllness", Description="Description3" },
                 new Illness() {Name = "BlackIllness", Description="Description4" },
                 new Illness() {Name = "Illness1Symptoms", Description="Description5" },
                 new Illness() {Name = "Illness2Symptoms", Description="Description6" },
            };

            context.Illnesses.AddRange(illnesses);
            context.SaveChanges();

            //Seeding with Crops
            var symptoms = new[]
            {
                 new Symptom() {Name = "z", Description="Descriptionz" },
                 new Symptom() {Name = "White leaf", Description="Description1" },
                 new Symptom() {Name = "Yellow leaf", Description="Description2" },
                 new Symptom() {Name = "Dots", Description="Description3" },
                 new Symptom() {Name = "Small leaf", Description="Description1" },
                 new Symptom() {Name = "SmthElse", Description="Description1" },
                 new Symptom() {Name = "f", Description="Description2" },
                 new Symptom() {Name = "g", Description="Description3" },
                 new Symptom() {Name = "h", Description="Description1" },
            };
            context.Symptoms.AddRange(symptoms);
            context.SaveChanges();

            var illnessSymptoms = new[]
{
                new IllnessSymptom(){ Illness = illnesses[0], Symptom = symptoms[0] },
                new IllnessSymptom(){ Illness = illnesses[0], Symptom = symptoms[1] },
                new IllnessSymptom(){ Illness = illnesses[0], Symptom = symptoms[2] },
                new IllnessSymptom(){ Illness = illnesses[0], Symptom = symptoms[3] },

                //new IllnessSymptom(){ Illness = illnesses[1], Symptom = symptoms[0] },
                //new IllnessSymptom(){ Illness = illnesses[1], Symptom = symptoms[1] },
                //new IllnessSymptom(){ Illness = illnesses[1], Symptom = symptoms[2] },

                new IllnessSymptom(){ Illness = illnesses[2], Symptom = symptoms[1] },
                new IllnessSymptom(){ Illness = illnesses[2], Symptom = symptoms[2] },
                new IllnessSymptom(){ Illness = illnesses[2], Symptom = symptoms[3] },

                //new IllnessSymptom(){ Illness = illnesses[3], Symptom = symptoms[1] },
                //new IllnessSymptom(){ Illness = illnesses[3], Symptom = symptoms[2] },

                new IllnessSymptom(){ Illness = illnesses[4], Symptom = symptoms[1] },
                new IllnessSymptom(){ Illness = illnesses[4], Symptom = symptoms[2] },
                new IllnessSymptom(){ Illness = illnesses[4], Symptom = symptoms[4] },
                new IllnessSymptom(){ Illness = illnesses[4], Symptom = symptoms[5] },

                new IllnessSymptom(){ Illness = illnesses[5], Symptom = symptoms[1] },
                new IllnessSymptom(){ Illness = illnesses[5], Symptom = symptoms[2] },
                new IllnessSymptom(){ Illness = illnesses[5], Symptom = symptoms[3] },
                new IllnessSymptom(){ Illness = illnesses[5], Symptom = symptoms[4] },

            };
            context.IllnessSymptoms.AddRange(illnessSymptoms);
            context.SaveChanges();

            var cropProducts = new[]
            {
                new CropProduct(){ Product = products[0], Crop = crops[0]},
                new CropProduct(){ Product = products[0], Crop = crops[1]},
                new CropProduct(){ Product = products[0], Crop = crops[2]},
                new CropProduct(){ Product = products[1], Crop = crops[0]},
                new CropProduct(){ Product = products[2], Crop = crops[0]},
                new CropProduct(){ Product = products[2], Crop = crops[1]},

            };
            context.CropProducts.AddRange(cropProducts);
            context.SaveChanges();

            var cropIllnesses= new[]
            {
                new CropIllness(){ Crop = crops[0], Illness = illnesses[0]},
                new CropIllness(){ Crop = crops[1], Illness = illnesses[0]},
                new CropIllness(){ Crop = crops[2], Illness = illnesses[1]},
                new CropIllness(){ Crop = crops[0], Illness = illnesses[1]},
            };
            context.CropIllnesses.AddRange(cropIllnesses);
            context.SaveChanges();

            var productIllnesses = new[]
            {
                new ProductIllness(){ Product = products[0], Illness = illnesses[0]},
                new ProductIllness(){ Product = products[0], Illness = illnesses[1]},
                new ProductIllness(){ Product = products[0], Illness = illnesses[2]},
                new ProductIllness(){ Product = products[1], Illness = illnesses[0]},
                new ProductIllness(){ Product = products[1], Illness = illnesses[1]},
                new ProductIllness(){ Product = products[1], Illness = illnesses[2]},
                new ProductIllness(){ Product = products[2], Illness = illnesses[2]},
                new ProductIllness(){ Product = products[2], Illness = illnesses[1]}
            };
            context.ProductIllnesses.AddRange(productIllnesses);
            context.SaveChanges();
        }
    }
}
