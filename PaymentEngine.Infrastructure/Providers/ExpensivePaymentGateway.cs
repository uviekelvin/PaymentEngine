using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Providers.Interfaces;
using PaymentEngine.Infrastructure.Providers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.Providers
{
    public class ExpensivePaymentGateway : IPaymentGateway
    {
        public bool IsValid(decimal amount)
        {
            return amount > 20 && amount <= 500;
        }

        public ApiResponseDtos<bool> MakePayment(PaymentModel model)
        {
            var random = new Random().Next(0, 10);
            if (random > 5)
                return new ApiResponseDtos<bool> { Code = ApiResponseCodes.INVALID_REQUEST, Payload = false, Message = "Payment Failed" };
            else
                return new ApiResponseDtos<bool> { Code = ApiResponseCodes.OK, Payload = true, Message = "Payment Successful" };
        }

    }
}
