using System;
using Exemplo.Models;
using Exemplo.Models.Products;
using Exemplo.Models.Products.Enums;
using Exemplo.Services;

namespace Exemplo
{
    class Program
    {
        static Clothing CamisaVermelha = new Clothing {
            Id = 332,
            SKU = "00SNKES-7723",
            Stock = 42,
            Name = "Camisa Polo Vermelha",
            Brand = "Lacoste",
            Details = "Camisa polo vermelha lisa Lacoste",
            Size = "M",
            AgeGroup = AgeGroup.Adult,
            Colors = new Color[] {Color.Red},
            Materials = new Material[] {Material.Cotton, Material.Polyester},
            UnitPrice = 219.99M,
            Volume = new VolumeCM {
                Height = 70,
                Width = 54,
                Depth = 0.1
            },
            WeightKG = 0.15
        };

        static Furniture MesaJantar = new Furniture {
            Id = 1198,
            SKU = "pdnMRgdlhG9RJq53MS9T",
            Stock = 9,
            Name = "Mesa 160x80",
            Brand = "Tok&Stok",
            Details = "Mesa de MDF de eucalipto com tampo branco",
            Colors = new Color[] {Color.White},
            Materials = new Material[] {Material.FibreBoard},
            UnitPrice = 759.99M,
            Volume = new VolumeCM {
                Height = 76.5,
                Width = 160,
                Depth = 80
            },
            WeightKG = 32
        };

        static CreditPaymentService CreditPaymentService;
        
        static void Main(string[] args)
        {
            var compra = new Purchase("edu");
            
            compra.Cart.AddItem(CamisaVermelha, 3);
            compra.Cart.AddItem(MesaJantar);

            var status = compra.CompletePurchase(PaymentMethod.Credit).Result;

            Console.WriteLine(status);
            Console.WriteLine(compra.Cart.GetTotalValue());
        }
    }
}
