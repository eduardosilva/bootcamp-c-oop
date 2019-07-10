using System.Collections.Generic;
using System.Linq;
using Exemplo.Models.Products;

namespace Exemplo.Models {
    public class Cart {
        public IDictionary<int, Item> Products { get; }

        public Cart() {
            this.Products = new Dictionary<int, Item>();
        }

        public decimal GetTotal() {
            return Products.Sum(p => p.Value.SubTotal);
        }
    }

    public class Item {
        private int amount;

        public IProduct Product { get; }
        public int Amount {
            get {
                return amount;
            }
            set {
               if (value < 0) {
                   amount = 0;
               } else {
                   amount = value;
               }
            }
        }

        public decimal SubTotal {
            get {
                return amount * Product.GetUnitPrice();
            }
        }

        public Item(IProduct product, int amount = 1) {
            this.Product = product;
            this.Amount = amount;
        }
    }
}