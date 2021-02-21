using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.Providers.Models
{
    public class PaymentModel
    {
        public string CreditCardNumber { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
