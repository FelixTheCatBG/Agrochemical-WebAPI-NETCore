using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class ProductIllness
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int IllnessId { get; set; }

        public Illness Illness { get; set; }
    }
}
