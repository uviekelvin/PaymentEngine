using PaymentEngine.Api.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentEngine.Api.Dtos
{
    public class ProcessPaymentDtos
    {
        [Required(ErrorMessage = "CreditCardNumber is required")]
        [CreditCardValidation(AcceptedCardTypes = CreditCardValidationAttribute.CardType.All)]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "Cardholder is required")]
        public string CardHolder { get; set; }
        [CardExpiration]
        public DateTime ExpirationDate { get; set; }
        [RegularExpression(@"^[0-9]{3,3}$", ErrorMessage = "Secuity code must be min and max of 3 digits ")]
        public string SecurityCode { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }
    }
}
