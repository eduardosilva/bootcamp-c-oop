using System.Threading.Tasks;

namespace Exemplo.Services
{
    public interface IPaymentService
    {
        Task<bool> ExecutePayment(string document, decimal value);
    }
}