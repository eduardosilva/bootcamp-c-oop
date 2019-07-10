using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Models.Products;
using Exemplo.Models.Products.Enums;

namespace Exemplo.Models.Products
{
    public class Furniture: Product<string>
    {
        public FurnitureType Type { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Material> Materials { get; set; }
        public IEnumerable<string> Pattern { get; set; }
        public Size Size { get; set; }
        public double Weight;

        public override string GetDescription() {
            string colors = String.Join(" ", Colors.Select(c => c.GetDescription()));

            return $"{Name} {colors} {Brand} - {Details}";
        }
    }
}
