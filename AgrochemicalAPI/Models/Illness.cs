using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class Illness
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<CropIllness> CropIllnesses { get; set; } = new HashSet<CropIllness>();

        public ICollection<ProductIllness> ProductIllnesses { get; set; } = new HashSet<ProductIllness>();

        public ICollection<IllnessSymptom> IllnessSymptoms { get; set; } = new HashSet<IllnessSymptom>();

        public ICollection<ProductCropIllness> ProductCropIllnesses { get; set; } = new HashSet<ProductCropIllness>();

    }
}
