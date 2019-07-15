using Exemplo.Models.Products.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exemplo._3
{
    public class Product
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Details { get; set; }
        public double WeightKG { get; set; }
        public virtual string Description
        {
            get
            {
                return $"{Name} {Brand} - {Details}";
            }
        }
    }

    public class Clothing : Product
    {
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Material> Materials { get; set; }
        public IEnumerable<string> Patterns { get; set; }
        public AgeGroup AgeGroup { get; set; }
        public string Size { get; set; }
    }
}
