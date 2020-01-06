using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class Crop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CropCategoryId { get; set; }
        public CropCategory CropCategory { get; set; }
        public ICollection<CropProduct> CropProducts { get; set; } = new HashSet<CropProduct>();
        public ICollection<CropIllness> CropIllnesses { get; set; } = new HashSet<CropIllness>();

        public ICollection<ProductCropIllness> ProductCropIllnesses { get; set; } = new HashSet<ProductCropIllness>();
    }
}
