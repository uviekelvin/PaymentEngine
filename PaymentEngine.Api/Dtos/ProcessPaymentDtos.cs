using PaymentEngine.Api.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentEngine.Api.Dtos
{
    public class ProcessPaymentDtos
    {
        [Required(ErrorMessage = "CreditCardNumber is required")]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "Cardholder is required")]
        public string CardHolder { get; set; }
        [CardExpiration]
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }
    }
}
