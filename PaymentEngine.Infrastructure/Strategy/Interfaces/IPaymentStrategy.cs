using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Providers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.Strategy.Interfaces
{
    public interface IPaymentStrategy
    {
        ApiResponseDtos<bool> ProcessPayment(PaymentModel paymentModel);

    }
}
