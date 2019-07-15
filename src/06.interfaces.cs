using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exemplo._6
{
    public class Cart
    {
        private readonly IList<CartItem> items;
        public IEnumerable<CartItem> Items
        {
            get
            {
                return items;
            }
        }

        public Cart()
        {
            items = new List<CartItem>();
        }


        public void Add(IProduct product, int amount)
        {
            items.Add(new CartItem(product, amount));
        }

        public decimal Total()
        {
            return items.Sum(i => i.Partial());
        }
    }

    public class CartItem
    {
        public CartItem(IProduct product, int amount)
        {
            Product = product;
            Amount = amount;
        }

        public IProduct Product { get; set; }
        public int Amount { get; set; }

        public decimal Partial()
        {
            return Product.UnitPrice * Amount;
        }
    }

    public interface IProduct
    {
        decimal UnitPrice { get; set; }
    }

    public abstract class Product : IProduct
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
}
