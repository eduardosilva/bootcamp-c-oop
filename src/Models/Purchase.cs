using System;
using System.Threading.Tasks;
using Exemplo.Services;

namespace Exemplo.Models {

    public class Purchase {
        public int Id { get; protected set; }
        public string User { get; }
        public DateTime CreationDate { get; }
        public DateTime? CompletionDate { get; protected set; }
        public PurchaseStatus Status { get; protected set; }        
        public Cart Cart { get; }

        public Purchase(string user) {
            this.Id = 1;
            this.User = user;
            this.Cart = new Cart();
            this.CreationDate = DateTime.Now;
            this.Status = PurchaseStatus.InProgress;
        }
        
        public async Task<PurchaseStatus> CompletePurchase(PaymentMethod method) {
            if (this.Status != PurchaseStatus.InProgress) {
                throw new InvalidOperationException("Purchase already complete");
            }

            if (this.Cart.GetTotalAmount() == 0 ) {
                throw new InvalidOperationException("No items selected");
            }

            var factory = new PaymentServiceFactory();
            var service = factory.CreatePaymentService(method);

            if (await service.ExecuteTransaction(this.User, this.Cart)) {
                foreach (var item in this.Cart.Items)
                {
                    item.Product.Stock -= item.Amount;
                }
                this.Status = PurchaseStatus.Finished;
            } else {
                this.Status = PurchaseStatus.Failed;
            }

            this.CompletionDate = DateTime.Now;

            return this.Status;
        }
    }

    public enum PurchaseStatus {
        InProgress = 0,
        Finished = 1,
        Failed = 2
    }
}