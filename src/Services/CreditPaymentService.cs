using System;
using System.Threading.Tasks;
using Exemplo.Models;

namespace Exemplo.Services {
    public class CreditPaymentService : IPaymentService
    {
        private Action<string> log;

        public CreditPaymentService(Action<string> log) {
            this.log = log;
        }

        public async Task<bool> ExecuteTransaction(string user, Cart cart)
        {
            return await Task.Run(() => {
                log($"Beggining new transaction for user {user}");
                log("Items in cart:");
                foreach (var item in cart.Items)
                {
                    var value = item.Value;
                    log($"\t({value.Amount}) {value.Product.GetDescription()}");
                }
                return true;
            });
        }
    }
}