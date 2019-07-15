using System;
using System.Collections.Generic;
using System.Text;

namespace Exemplo._2
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
        public string Size { get; set; }
    }
}
