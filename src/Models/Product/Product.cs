using System;

namespace Exemplo.Models.Products
{
    public abstract class Product<T>: IProduct
        where T: IComparable
    {
        public int Id { get; set; }
        public T SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Details { get; set; }
        public VolumeCM Volume { get; set; }
        public double WeightKG { get; set; }
        public virtual string Description {
            get {
                return $"{Name} {Brand} - {Details}";
            }
        }
    }

    public struct VolumeCM {
        public double Height;
        public double Width;
        public double Depth;
    }
}
