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
                    new ProductCategory() {Name = "Fungicide"},
                    new ProductCategory() {Name = "Herbecide"},
                    new ProductCategory() {Name = "Insecticide"}
            };

            var products = new[]
            {
                new Product() {
                    Name="Diagonal",
                    ActiveIngredient="Azoxystrobin 250g/l",
                    Description="Description1",
                    Manufacturer = "Albaugh",
                    MechanismOfAction="A combination of two active substances with a different mode of action. " +
                    "Petoxamide is a systemic herbicide in the group of chloracetamides that is taken up by the roots of a sprouting plant." +
                    " Petoxamide inhibits weed emergence by inhibiting long-chain fatty acid synthesis and inhibiting cell division.",
                    ProductCategory=productCategories[0]
                },
                new Product() {
                    Name="Domnik 250",
                    ActiveIngredient="Tebuconazole 250 g / l",
                    Description="DOMNIK 250 EB is a systemic triazole fungicide for use in vineyards.",
                    Manufacturer = "SC",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[0]
                },
                new Product() {
                    Name="Impact 25 SK",
                    ActiveIngredient="Flutriafol 250 g / l",
                    Manufacturer = "FMC",
                    Description="Description1",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[0]
                },
                new Product() {
                    Name="Buzin",
                    ActiveIngredient="Azoxystrobin",
                    Description="Description1",
                    Manufacturer = "SC",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[1]
                },
                new Product() {
                    Name="Nero ™ EC",
                    ActiveIngredient="400 g / l petoxamide + 24 g / l clomazone",
                    Manufacturer = "FMC",
                    Description="Description1",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[1]
                },
                new Product() {
                    Name="CLIOFAR 600 SL",
                    ActiveIngredient="Clopyralid 600 g / l",
                    Manufacturer = "Arysta",
                    Description="Description1",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[1]
                },
                new Product() {
                    Name="DOMINATOR ULTRA",
                    ActiveIngredient="glyphosate 360 g / l",
                    Description="Description1",
                    Manufacturer= "Albaugh",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[1]
                },
                new Product() {
                    Name="FASTRIN 15 VG",
                    ActiveIngredient="Alpha-Cypermetrine 150g / kg",
                    Description="Description1",
                    Manufacturer = "SC",
                    MechanismOfAction="Non-systemic contact and gastric pyrethroid insecticide",
                    ProductCategory=productCategories[2]
                },
                new Product() {
                    Name="CORAGEN 20 SC",
                    ActiveIngredient="Azoxystrobin",
                    Manufacturer = "FMC",
                    Description="Description1",
                    MechanismOfAction="Non-systemic contact and gastric pyrethroid insecticide",
                    ProductCategory=productCategories[2]
                },
                new Product() {Name="Agrochemical102",Description="Description3", ProductCategory=productCategories[1]},
                new Product() {Name="Agrochemical105",Description="Description1", ProductCategory=productCategories[1]},
                new Product() {Name="Agrochemical106",Description="Description1", ProductCategory=productCategories[1]},
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

            //Seeding with Crops
            var crops = new[]
            {
                 new Crop() {Name = "Cucumbers", },
                 new Crop() {Name = "Melons",  },
                 new Crop() {Name = "Pumpkins",  },
                 new Crop() {Name = "Watermelons ",  },
                 new Crop() {Name = "Tomatoes",  },
                 new Crop() {Name = "Wheat",  },
                 new Crop() {Name = "Barley",  },
                 new Crop() {Name = "Sugar beet", }, 
                 new Crop() {Name = "Rice",  },
                 new Crop() {Name = "Vineyards"}
            };
            context.Crops.AddRange(crops);
            context.SaveChanges();

            //Seeding with Diseases
            var diseases = new[]
            {
                 new Disease() {Name = "Powdery mildew", Description="Description1" },
                 new Disease() {Name = "Brown Rust", Description="Description2" },
                 new Disease() {Name = "Yellow Rust", Description="Description3" },
                 new Disease() {Name = "Rusts", Description="Description3" },
                 new Disease() {Name = "Church sporosis", Description="Description4" },
                 new Disease() {Name = "Septoria", Description="Description5" },
                 new Disease() {Name = "LastIllness", Description="Description6" },
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

                new DiseaseSymptom(){ Disease = diseases[2], Symptom = symptoms[1] },
                new DiseaseSymptom(){ Disease = diseases[2], Symptom = symptoms[2] },
                new DiseaseSymptom(){ Disease = diseases[2], Symptom = symptoms[3] },


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
 
           var productCropDiseases = new[]
            {
                new ProductCropDisease(){Product = products[0], Crop = crops[0], Disease = diseases[0], Dosage = "80ml / decare", Period="1 day"},
                new ProductCropDisease(){Product = products[0], Crop = crops[1], Disease = diseases[0], Dosage = "80ml / decare", Period="3 days"},
                new ProductCropDisease(){Product = products[0], Crop = crops[2], Disease = diseases[0], Dosage = "80ml / decare", Period="3 days"},
                new ProductCropDisease(){Product = products[0], Crop = crops[3], Disease = diseases[1], Dosage = "80ml / decare",Period="3 days"},
                new ProductCropDisease(){Product = products[0], Crop = crops[4], Disease = diseases[3], Dosage = "80-100ml / decare", Period="3 days"},
                new ProductCropDisease(){Product = products[0], Crop = crops[5], Disease = diseases[4], Dosage = "100ml / decare",Period="35 days"},
                new ProductCropDisease(){Product = products[0], Crop = crops[6], Disease = diseases[1], Dosage = "80ml / decare",Period="35 days"},
                new ProductCropDisease(){Product = products[1], Crop = crops[9], Disease = diseases[0], Dosage="40 ml / decare", Period="14 days"},
                new ProductCropDisease(){Product = products[1], Crop = crops[5], Disease = diseases[3], Dosage = "100ml / decare", Period="30 days"},
                new ProductCropDisease(){Product = products[1], Crop = crops[6], Disease = diseases[3], Dosage="100ml / decare", Period="30 days"},
                new ProductCropDisease(){Product = products[2], Crop = crops[7], Disease = diseases[3], Dosage="25 ml / decare", Period="30 days"},
                new ProductCropDisease(){Product = products[2], Crop = crops[6], Disease = diseases[0], Dosage="50 ml / decare", Period="30 days"},
                new ProductCropDisease(){Product = products[2], Crop = crops[6], Disease = diseases[5], Dosage="50 ml / decare", Period="30 days"},
            };

            context.ProductCropDiseases.AddRange(productCropDiseases);
            context.SaveChanges();

        }
    }
}
