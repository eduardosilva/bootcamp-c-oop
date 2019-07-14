using System.Collections.Generic;
using System.Linq;
using Exemplo.Models.Products;

namespace Exemplo.Models {
    public class CartItem {
        private int amount;

        public IProduct Product { get; }
        public int Amount {
            get {
                return amount;
            }
            set {
               if (value < 0) {
                   amount = 0;
               } else if (value > Product.Stock) {
                   amount = Product.Stock;
               } else {
                   amount = value;
               }
            }
        }

        public decimal SubTotal {
            get {
                return amount * Product.UnitPrice;
            }
        }

        public CartItem(IProduct product, int amount = 1) {
            this.Product = product;
            this.Amount = amount;
        }
    }
}