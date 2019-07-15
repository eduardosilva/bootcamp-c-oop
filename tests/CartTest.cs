using System;
using Xunit;
using Exemplo.Models;
using Exemplo.Models.Products;
using Exemplo.Models.Products.Enums;
using System.Linq;

namespace tests
{
    public class CartTest
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

        [Fact]
        public void AfterAddItem_ProductIdShouldMatch()
        {
            var cart = new Cart();

            cart.AddItem(MesaJantar);
            
            Assert.Equal(MesaJantar.Id,cart.Items.FirstOrDefault().Product.Id);
        }

        [Fact]
        public void AfterAddItem_GetTotalAmountShouldReturnThree()
        {
            var cart = new Cart();

            cart.AddItem(MesaJantar,3);
            
            Assert.Equal(3,cart.GetTotalAmount());
        }

        [Fact]
        public void AfterAddItem_GetTotalValueShouldMatch()
        {
            var cart = new Cart();

            cart.AddItem(MesaJantar,2);
            cart.AddItem(CamisaVermelha,3);

            Assert.Equal(2179.95M, cart.GetTotalValue());
        }

    }

}
