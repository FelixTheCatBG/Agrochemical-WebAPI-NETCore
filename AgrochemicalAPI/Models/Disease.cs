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

        public string Treatment { get; set; }

        public string SymptomsDescription { get; set; }
        public string imgPath { get; set; }

        public ICollection<DiseaseSymptom> DiseaseSymtpoms { get; set; } = new HashSet<DiseaseSymptom>();

        public ICollection<ProductCropDisease> ProductCropDiseases { get; set; } = new HashSet<ProductCropDisease>();

    }
}
