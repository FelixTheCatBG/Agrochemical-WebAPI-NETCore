using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class ProductCropIllness
    {
            public int ProductCropIllnessId { get; set; }
            public int ProductId { get; set; }
            public int CropId { get; set; }
            public int IllnessId { get; set; }
            public Product Product { get; set; }
            public Crop Crop { get; set; }
            public Illness Illness { get; set; }
            public string Dosage { get; set; }

    }
}
