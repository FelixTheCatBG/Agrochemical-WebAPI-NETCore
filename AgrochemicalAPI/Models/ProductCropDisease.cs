using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class ProductCropDisease
    {
            public int ProductCropDiseaseId { get; set; }
            public int ProductId { get; set; }
            public int CropId { get; set; }
            public int DiseaseId { get; set; }
            public Product Product { get; set; }
            public Crop Crop { get; set; }
            public Disease Disease { get; set; }
            public string Dosage { get; set; }
            public string Period { get; set; }

    }
}
