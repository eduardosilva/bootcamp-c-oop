using System;
using Xunit;
using Exemplo.Models;
using Exemplo.Models.Products;
using Exemplo.Models.Products.Enums;
using System.Linq;
using Exemplo.Services;
using System.Threading.Tasks;

namespace tests
{
    public class PurchaseTest
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

        [Fact]
        public async Task AfterCompletePurchase_PurchaseStatusShouldBeFinished()
        {
            var compra = new Purchase("teste");

            compra.Cart.AddItem(CamisaVermelha);
            await compra.CompletePurchase(PaymentMethod.Credit);

            Assert.Equal(PurchaseStatus.Finished,compra.Status);
        }
    }

}
