using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class ProductAdvantage
    {
        public int Id { get; set; }
        public string Advantage { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
