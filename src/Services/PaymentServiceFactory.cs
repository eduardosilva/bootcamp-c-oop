using System;
using System.Collections.Generic;

namespace Exemplo.Services
{

    public class PaymentServiceFactory
    {
        private IDictionary<PaymentMethod, Type> options;

        public PaymentServiceFactory()
        {
            this.options = new Dictionary<PaymentMethod, Type>() {
                { PaymentMethod.Credit, typeof(CreditPaymentService) }
            };
        }

        public IPaymentService CreatePaymentService(PaymentMethod paymentMethod)
        {
            var args = new object[] { new Action<string>((m) => Console.WriteLine(m)) };
            return (IPaymentService)Activator.CreateInstance(this.options[paymentMethod], args);
        }
    }

    public enum PaymentMethod
    {
        Credit = 1,
        Debit = 2,
        BankSlip = 3
    }
}