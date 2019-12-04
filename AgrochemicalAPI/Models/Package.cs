using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class Package
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
