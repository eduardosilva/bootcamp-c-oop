using System.Threading.Tasks;
using Exemplo.Models;

namespace Exemplo.Services {
    public interface IPaymentService {
        Task<bool> ExecuteTransaction(string user, Cart cart);
    }
}