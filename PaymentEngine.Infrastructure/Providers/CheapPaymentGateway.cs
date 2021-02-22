using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Providers.Interfaces;
using PaymentEngine.Infrastructure.Providers.Models;
using System;

namespace PaymentEngine.Infrastructure.Providers
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        public bool IsValid(decimal amount)
        {
            return amount < 20;
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
