using System;
using System.Threading.Tasks;
using Exemplo.Services.Logger;

namespace Exemplo.Services
{
    public interface IPaymentService : ILog
    {
        Task<bool> ExecutePayment(string document, decimal value);
    }
}