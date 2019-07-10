using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Models.Products.Enums;

namespace Exemplo.Models.Products
{
    public class Clothing: Product<string>
    {
        // Diretamente da especificação do Google Merchant para produtos
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Material> Materials { get; set; }
        public IEnumerable<string> Patterns { get; set; }
        public AgeGroup AgeGroup { get; set; }
        public string Size { get; set; }

        public override string GetDescription() {
            string colors = String.Join(" ", Colors.Select(c => c.GetDescription()));

            return $"{Name} ({colors}) {Brand} - {Details}";
        }
    }
}
