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

        public string Description { get; set; }

        public string Dose { get; set; }

        public int ProductCategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public ICollection<Package> Packages { get; set; } = new HashSet<Package>();

        public ICollection<CropProduct> CropProducts { get; set; } = new HashSet<CropProduct>();

        public ICollection<ProductIllness> ProductIllnesses { get; set; } = new HashSet<ProductIllness>();

    }
}
