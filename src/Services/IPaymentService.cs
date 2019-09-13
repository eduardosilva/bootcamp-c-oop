using System;
using System.Threading.Tasks;

namespace Exemplo.Services
{
    public interface IPaymentService
    {
        Action<string> Logger { get; }
        Task<bool> ExecutePayment(string document, decimal value);
    }
}