using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Models.Products;

namespace Exemplo.Models {
    public class Cart {
        protected IDictionary<int, CartItem> items;

        public IEnumerable<CartItem> Items {
            get {
                return this.items.Values;
            }
        }

        public Cart() {
            this.items = new Dictionary<int, CartItem>();
        }

        public int AddItem(IProduct product, int amount = 1) {
            CartItem item = null;

            if (!items.TryGetValue(product.Id, out item)) {
                if (product.Stock == 0 ) {
                    throw new InvalidOperationException("Product not available");
                }
                
                item = new CartItem(product, 0);
                this.items.Add(product.Id, item);
            }

            return item.Amount += amount;
        }

        public int RemoveItem(IProduct product, int amount = 1) {
            return RemoveItem(product.Id, amount);
        }

        public int RemoveItem(int id, int amount = 1) {
            var item = this.items[id];

            item.Amount -= amount;

            if (item.Amount == 0 ) {
                this.items.Remove(id);
            }

            return item.Amount;
        }

        public void SetAmount(IProduct product, int amount) {
            SetAmount(product.Id, amount);
        }

        public void SetAmount(int id, int amount) {
            var item = this.items[id];

            item.Amount = amount;

            if (item.Amount == 0 ) {
                this.items.Remove(id);
            }
        }

        public decimal GetTotalValue() {
            return this.items.Sum(p => p.Value.SubTotal);
        }

        public decimal GetTotalAmount() {
            return this.items.Sum(p => p.Value.Amount);
        }
    }

}