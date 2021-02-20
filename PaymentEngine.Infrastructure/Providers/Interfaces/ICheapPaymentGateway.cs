using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Infrastructure.Providers.Interfaces
{
    public interface ICheapPaymentGateway
    {
        Task<string> ProcessPayment(string CreditCardNumber, string Cvv, decimal amount);
    }
}
