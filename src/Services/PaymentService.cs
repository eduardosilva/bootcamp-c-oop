using System.Threading.Tasks;

namespace Exemplo.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> ExecutePayment(string document, decimal value)
        {
            // I made the payment ok trust me 🤡

            return Task.FromResult(true);
        }
    }
}
