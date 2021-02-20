using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Infrastructure.Providers.Interfaces
{
    public interface IExpensivePaymentGateway
    {
        Task<string> ProcessPayment(string CreditCardNumber, string Cvv, decimal amount);
    }
}
