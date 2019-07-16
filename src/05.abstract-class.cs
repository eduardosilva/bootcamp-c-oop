using Exemplo.Models.Products.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exemplo._5
{
    public abstract class Product
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

        //override Example
        public override string Description
        {
            get
            {
                string colors = String.Join(" ", Colors.Select(c => c.GetDescription()));
                return $"{Name} ({colors}) {Brand} - {Details}";
            }
        }
    }

    public class Table : Product
    {
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Material> Materials { get; set; }
        public IEnumerable<string> Pattern { get; set; }
        public override string Description
        {
            get
            {
                string colors = String.Join(" ", Colors.Select(c => c.GetDescription()));

                return $"{Name} {colors} {Brand} - {Details}";
            }
        }
    }
}
