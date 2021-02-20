using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Domain.Models
{
    public class Payments
    {
        public long Id { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
    }
}
