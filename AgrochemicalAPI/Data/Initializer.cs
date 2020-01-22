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
            var diseases = new[]
            {
                 new Disease() {Name = "WhiteIllness", Description="Description1" },
                 new Disease() {Name = "YellowIllness", Description="Description2" },
                 new Disease() {Name = "RedIllness", Description="Description3" },
                 new Disease() {Name = "BlackIllness", Description="Description4" },
                 new Disease() {Name = "Illness1Symptoms", Description="Description5" },
                 new Disease() {Name = "Illness2Symptoms", Description="Description6" },
            };

            context.Diseases.AddRange(diseases);
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

            var diseaseSymptoms = new[]
{
                new DiseaseSymptom(){ Disease = diseases[0], Symptom = symptoms[0] },
                new DiseaseSymptom(){ Disease = diseases[0], Symptom = symptoms[1] },
                new DiseaseSymptom(){ Disease = diseases[0], Symptom = symptoms[2] },
                new DiseaseSymptom(){ Disease = diseases[0], Symptom = symptoms[3] },

                //new IllnessSymptom(){ Disease = illnesses[1], Symptom = symptoms[0] },
                //new IllnessSymptom(){ Disease = illnesses[1], Symptom = symptoms[1] },
                //new IllnessSymptom(){ Disease = illnesses[1], Symptom = symptoms[2] },

                new DiseaseSymptom(){ Disease = diseases[2], Symptom = symptoms[1] },
                new DiseaseSymptom(){ Disease = diseases[2], Symptom = symptoms[2] },
                new DiseaseSymptom(){ Disease = diseases[2], Symptom = symptoms[3] },

                //new IllnessSymptom(){ Disease = illnesses[3], Symptom = symptoms[1] },
                //new IllnessSymptom(){ Disease = illnesses[3], Symptom = symptoms[2] },

                new DiseaseSymptom(){ Disease = diseases[4], Symptom = symptoms[1] },
                new DiseaseSymptom(){ Disease = diseases[4], Symptom = symptoms[2] },
                new DiseaseSymptom(){ Disease = diseases[4], Symptom = symptoms[4] },
                new DiseaseSymptom(){ Disease = diseases[4], Symptom = symptoms[5] },

                new DiseaseSymptom(){ Disease = diseases[5], Symptom = symptoms[1] },
                new DiseaseSymptom(){ Disease = diseases[5], Symptom = symptoms[2] },
                new DiseaseSymptom(){ Disease = diseases[5], Symptom = symptoms[3] },
                new DiseaseSymptom(){ Disease = diseases[5], Symptom = symptoms[4] },

            };
            context.DiseaseSymptoms.AddRange(diseaseSymptoms);
            context.SaveChanges();

            var cropProducts = new[]
            {
                new CropProduct(){ Product = products[0], Crop = crops[0], Dose = "SOME DOSE 1"},
                new CropProduct(){ Product = products[0], Crop = crops[1], Dose = "SOME DOSE 2"},
                new CropProduct(){ Product = products[0], Crop = crops[2], Dose = "SOME DOSE 3"},
                new CropProduct(){ Product = products[1], Crop = crops[0], Dose = "SOME DOSE 4"},
                new CropProduct(){ Product = products[2], Crop = crops[0], Dose = "SOME DOSE 5"},
                new CropProduct(){ Product = products[2], Crop = crops[1], Dose = "SOME DOSE 7"},

            };
            context.CropProducts.AddRange(cropProducts);
            context.SaveChanges();

            var cropDiseases= new[]
            {
                new CropDisease(){ Crop = crops[0], Disease = diseases[0]},
                new CropDisease(){ Crop = crops[1], Disease = diseases[0]},
                new CropDisease(){ Crop = crops[2], Disease = diseases[1]},
                new CropDisease(){ Crop = crops[0], Disease = diseases[1]},
            };
            context.CropDiseases.AddRange(cropDiseases);
            context.SaveChanges();

            var productCropDiseases = new[]
{
                new ProductCropDisease(){Product = products[0], Crop = crops[0], Disease = diseases[0],Dosage="2323aaa"},
                new ProductCropDisease(){Product = products[0], Crop = crops[1], Disease = diseases[0],Dosage="2323aaa"},
                new ProductCropDisease(){ Product = products[1],Crop = crops[2], Disease = diseases[1],Dosage="2323aaa"},
                new ProductCropDisease(){ Product = products[1],Crop = crops[0], Disease = diseases[1],Dosage="2323aaa"},
            };
            context.ProductCropDiseases.AddRange(productCropDiseases);
            context.SaveChanges();

            var productDiseases = new[]
            {
                new ProductDisease(){ Product = products[0], Disease = diseases[0]},
                new ProductDisease(){ Product = products[0], Disease = diseases[1]},
                new ProductDisease(){ Product = products[0], Disease = diseases[2]},
                new ProductDisease(){ Product = products[1], Disease = diseases[0]},
                new ProductDisease(){ Product = products[1], Disease = diseases[1]},
                new ProductDisease(){ Product = products[1], Disease = diseases[2]},
                new ProductDisease(){ Product = products[2], Disease = diseases[2]},
                new ProductDisease(){ Product = products[2], Disease = diseases[1]}
            };
            context.ProductDiseases.AddRange(productDiseases);
            context.SaveChanges();
        }
    }
}
