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
            Name = "Camisa Polo Vermelha",
            Brand = "Lacoste",
            Details = "Camisa polo vermelha lisa Lacoste",
            Size = "M",
            AgeGroup = AgeGroup.Adult,
            Colors = new Color[] {Color.Red},
            Materials = new Material[] {Material.Cotton, Material.Polyester},
            UnitPrice = 219.99M
        };

        static Furniture MesaJantar = new Furniture {
            Id = 1198,
            SKU = "pdnMRgdlhG9RJq53MS9T",
            Name = "Mesa 160x80",
            Brand = "Tok&Stok",
            Details = "Mesa de MDF de eucalipto com tampo branco",
            Colors = new Color[] {Color.White},
            Materials = new Material[] {Material.FibreBoard},
            UnitPrice = 759.99M,
            Size = new Size {
                Height = 76.5,
                Width = 160,
                Depth = 80
            },
            Weight = 32
        };

        static CreditPaymentService CreditPaymentService;
        
        static void Main(string[] args)
        {
            CreditPaymentService = new CreditPaymentService(Console.WriteLine);

            var carrinho = new Cart();
            
            var item1 = new Item(CamisaVermelha, 3);
            var item2 = new Item(MesaJantar, 1);

            carrinho.Products.Add(item1.Product.Id, item1);
            carrinho.Products.Add(item2.Product.Id, item2);

            var ok = CreditPaymentService.ExecuteTransaction("edu", carrinho).Result;

            Console.WriteLine(ok);
            Console.WriteLine(carrinho.GetTotal());
        }
    }
}
