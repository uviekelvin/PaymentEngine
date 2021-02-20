using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentEngine.Infrastructure.Dtos
{
    public class ProcessPaymentResponse
    {
        public string PaymentReference { get; set; }
    }
}
