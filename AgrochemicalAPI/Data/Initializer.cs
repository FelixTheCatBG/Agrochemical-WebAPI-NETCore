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
            var manufacturers = new[]
{
                    new Manufacturerr() {Name = "Albaugh"},
                    new Manufacturerr() {Name = "SC"},
                    new Manufacturerr() {Name = "FMC"},
                    new Manufacturerr() {Name = "Arysta"},
            };

            //Seeding with products
            var products = new[]
            {
                new Product() {
                    Name="Diagonal",
                    ActiveIngredient="Azoxystrobin 250g/l",
                    Description="Diagonal is a systemic triazole fungicide for use in vineyards. It has protective, curative and eradicating action.",
                    Manufacturer = "Albaugh",
                    MechanismOfAction="A combination of two active substances with a different mode of action. " +
                    "Petoxamide is a systemic herbicide in the group of chloracetamides that is taken up by the roots of a sprouting plant." +
                    " Petoxamide inhibits weed emergence by inhibiting long-chain fatty acid synthesis and inhibiting cell division.",
                    HowToUseRecommendation="Do not spray if precipitation is expected within 4-5 hours after treatment. The treatment should be carried out in silence  +time, at a temperature not higher than 24 - 25 degrees. Use a clean solution for the preparation of the working solution water. " +
                    "For maximum effect, weeds must have developed enough foliage to be able to absorb the product well and reach all growth points.When spraying vines, the treatment should be directed to the weeds so that the working solution does not fall on the green parts of the vine.Take it all precautions so that Dominator Ultra does not fall on adjacent crops to the sprayed block.",
                    ProductCategory=productCategories[0],
                    Manufacturerr= manufacturers[0]
                },
                new Product() {
                    Name="Domnik 250",
                    ActiveIngredient="Tebuconazole 250 g / l",
                    Description="DOMNIK 250 EB is a systemic eradicant and protective fungicide of the DMI group, active against various fungal diseases on cucurbits, fruit and ornamentals.",
                    Manufacturer = "SC",
                    MechanismOfAction=" Domnik is  systemic action fungicide. The product suppresses the development of the pathogen by inhibiting the ergosterol biosynthesis required to form the cell membrane. As a result, structural and functional abnormalities in the cell wall of the fungal pathogen are induced and growth of the hyphae is stopped. As a fungicide with systemic action, IMPACT 25 SK penetrates the leaf mass and spreads throughout the plant through the conducting system, protecting the new growth as well.",                   
                    HowToUseRecommendation="Consumption of working solution: 15-20 l / ha .Domnik 25 SK should be applied before the onset of the first signs of the disease, or when the disease has covered no more than 2% of the leaf mass. Ensure full spraying of plants for maximum effect.Prepare the working solution immediately before spraying. Compatibility: Domnik 25 SC is mixed with most insecticides and fungicides used in practice, but before being mixed with another product, a compatibility test should be carried out in a separate container.",
                    ProductCategory =productCategories[0],
                    Manufacturerr= manufacturers[1]
                },
                new Product() {
                    Name="Impact 25 SK",
                    ActiveIngredient="Flutriafol 250 g / l",
                    Manufacturer = "FMC",
                    Description="Description1",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[0],
                    Manufacturerr= manufacturers[2]
                },
                new Product() {
                    Name="Buzin",
                    ActiveIngredient="Azoxystrobin",
                    Description="Description1",
                    Manufacturer = "SC",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[1],
                    Manufacturerr= manufacturers[1]
                },
                new Product() {
                    Name="Nero ™ EC",
                    ActiveIngredient="400 g / l petoxamide + 24 g / l clomazone",
                    Manufacturer = "FMC",
                    Description="Description1",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[1],
                    Manufacturerr= manufacturers[2]
                },
                new Product() {
                    Name="CLIOFAR 600 SL",
                    ActiveIngredient="Clopyralid 600 g / l",
                    Manufacturer = "Arysta",
                    Description="Description1",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[1],
                    Manufacturerr= manufacturers[3]
                },
                new Product() {
                    Name="DOMINATOR ULTRA",
                    ActiveIngredient="glyphosate 360 g / l",
                    Description="Description1",
                    Manufacturer= "Albaugh",
                    MechanismOfAction="Systemic strobilurin fungicide with protective, curative and eradicating action",
                    ProductCategory=productCategories[1],
                    Manufacturerr= manufacturers[0]
                },
                new Product() {
                    Name="FASTRIN 15 VG",
                    ActiveIngredient="Alpha-Cypermetrine 150g / kg",
                    Description="Description1",
                    Manufacturer = "SC",
                    MechanismOfAction="Non-systemic contact and gastric pyrethroid insecticide",
                    ProductCategory=productCategories[2],
                    Manufacturerr= manufacturers[1]
                },
                new Product() {
                    Name="CORAGEN 20 SC",
                    ActiveIngredient="Azoxystrobin",
                    Manufacturer = "FMC",
                    Description="Description1",
                    MechanismOfAction="Non-systemic contact and gastric pyrethroid insecticide",
                    ProductCategory=productCategories[2],
                    Manufacturerr= manufacturers[2]
                },
                new Product() {Name="Agrochemical102",Description="Description3", ProductCategory=productCategories[1], Manufacturerr= manufacturers[2]},
                new Product() {Name="Agrochemical105",Description="Description1", ProductCategory=productCategories[1], Manufacturerr= manufacturers[2]},
                new Product() {Name="Agrochemical106",Description="Description1", ProductCategory=productCategories[1], Manufacturerr= manufacturers[2]},
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
                 new Disease() {Name = "Powdery mildew", Description="Common on many plants and easily recognized, powdery mildew is a fungal disease found throughout the United States. It is caused by a variety of closely related fungal species, each with a limited host range. (The fungi attacking your roses are unlikely to spread to your lilacs). Low soil moisture combined with high humidity levels at the plant surface favors this disease.",
                 SymptomsDescription = "Symptoms usually appear later in the growing season on outdoor plants. Powdery mildew starts on young leaves as raised blister-like areas that cause leaves to curl, exposing the lower leaf surface. Infected leaves become covered with a white to gray powdery growth, usually on the upper surface; unopened flower buds may be white with mildew and may never open. Leaves of severely infected plants turn brown and drop." +
                 " The disease prefers young, succulent growth; mature leaves are usually not affected.",
                  Treatment ="Plant resistant cultivars in sunny locations whenever possible.Prune or stake plants to improve air circulation." +
                     " Make sure to disinfect your pruning tools (one part bleach to 4 parts water) after each cut.Remove diseased foliage from the plant and clean up fallen debris on the ground.Use a thick layer of mulch or organic compost to cover the soil after you have raked and cleaned it well. Mulch will prevent the disease spores from splashing back up onto the leaves." +
                 "Milk sprays, made with 40% milk and 60% water, are an effective home remedy for use on a wide range of plants.  + " +
                 "For best results, spray plant leaves as a preventative measure every 10-14 days.Wash foliage occasionally to disrupt the daily spore-releasing cycle. Neem oil and PM Wash, used on a 7 day schedule," +
                 " will prevent fungal attack on plants grown indoors.", imgPath= "mildew"},
                 new Disease() {Name = "Brown Rot", Description="The most common fungal disease affecting the blossoms and fruit of almonds, apricots, cherries, peaches and plums. Brown rot (Monilinia fructicola) overwinters in mummified fruit (on the tree and on the ground) and infected twigs.", 
                     SymptomsDescription = "The disease first infects blossoms in spring and grows back into the small branches to cause cankers that can kill stems. Large numbers of flower-bearing stems are killed when the disease is severe. Dead flowers often remain attached into the summer. Developing or mature fruits show circular or brown spots that spread rapidly over the surface and light gray masses of spores are produced on the rotted areas. Rotted tissue remains relatively firm and dry.",
                     Treatment ="Choose resistant varieties whenever possible.Prompt removal and destruction of infected plant parts helps breaks the life cycle of the disease in individual trees and small orchards, and may be sufficient to keep brown rot below damaging levels.It is important to rake up and remove any fallen fruit or debris from under trees.Prune trees occasionally to improve air circulation. Also, water from below to keep from wetting blossoms, foliage and fruit.", imgPath= "rot"},
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
                 new Symptom() {Name = "oval pustules with powdery contents", Description="Descriptionz" },
                 new Symptom() {Name = "darker-coloured winter spore deposit", Description="Description1" },
                 new Symptom() {Name = "epidermis of the leaf", Description="Description2" },
                 new Symptom() {Name = "raised blister-like areas", Description="Description3" },
                 new Symptom() {Name = "curled leafs", Description="Description1" },
                 new Symptom() {Name = "white to gray powdery growth, usually on the upper surface", Description="Description1" },
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
