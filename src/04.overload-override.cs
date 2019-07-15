using Exemplo.Models.Products.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exemplo._4
{
    public class Clothing : _3.Product
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

    public class Cart
    {
        private readonly IList<_3.Product> items;
        public IEnumerable<_3.Product> Items
        {
            get
            {
                return items;
            }
        }

        // constructor overloading
        public Cart()
        {
            items =  new List<_3.Product>();
        }

        // constructor overloading
        public Cart(IEnumerable<_3.Product> items) 
            : this()
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        // method overloading
        public void Add(_3.Product product)
        {
            items.Add(product);
        }

        // method overloading
        public void Add(IEnumerable<_3.Product> products)
        {
            foreach(var product in products)
            {
                this.items.Add(product);
            }
        }
    }
}
