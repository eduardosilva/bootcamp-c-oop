using System;

namespace Exemplo.Models.Products
{
    public abstract class Product<T>: IProduct
        where T: IComparable
    {
        public int Id { get; set; }
        public T SKU { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Details { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual string GetDescription() {
            return $"{Name} {Brand} - {Details}";
        }

        public virtual decimal GetUnitPrice() {
            return UnitPrice;
        }
    }

    public struct Size {
        public double Height;
        public double Width;
        public double Depth;
    }
}
