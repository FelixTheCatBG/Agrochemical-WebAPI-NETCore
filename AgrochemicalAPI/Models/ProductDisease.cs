using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class ProductDisease
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int DiseaseId { get; set; }

        public Disease Disease { get; set; }
    }
}
