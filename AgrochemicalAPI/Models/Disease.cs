using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class Disease
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<CropDisease> CropDiseases { get; set; } = new HashSet<CropDisease>();

        public ICollection<ProductDisease> ProductDiseases { get; set; } = new HashSet<ProductDisease>();

        public ICollection<DiseaseSymptom> DiseaseSymtpoms { get; set; } = new HashSet<DiseaseSymptom>();

        public ICollection<ProductCropDisease> ProductCropDiseases { get; set; } = new HashSet<ProductCropDisease>();

    }
}
