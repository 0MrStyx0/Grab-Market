using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabMarket.Models
{
    internal class Product
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid Code { get; set; }
        public string Unit { get; set; }
        public float? Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
