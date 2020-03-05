using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ActiveIngredient { get; set; }


        public string Description { get; set; }

        public string Manufacturer { get; set; }
        
        public string MechanismOfAction { get; set; }

        public string HowToUseRecommendation { get; set; }

        public int ProductCategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public int ManufacturerrId { get; set; }

        public Manufacturerr Manufacturerr { get; set; }

        public ICollection<Package> Packages { get; set; } = new HashSet<Package>();

        public ICollection<ProductAdvantage> ProductAdvantages { get; set; } = new HashSet<ProductAdvantage>();

        public ICollection<ProductCropDisease> ProductCropDiseases { get; set; } = new HashSet<ProductCropDisease>();

    }
}
