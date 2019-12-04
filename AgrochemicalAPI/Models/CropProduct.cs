using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class CropProduct
    {
        public int CropId { get; set; }
        public Crop Crop { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
