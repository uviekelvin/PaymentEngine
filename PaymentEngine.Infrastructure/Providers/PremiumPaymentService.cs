using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Providers.Interfaces;
using PaymentEngine.Infrastructure.Providers.Models;
using PaymentEngine.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.Providers
{
    public class PremiumPaymentService : IPaymentGateway
    {
        public bool IsValid(decimal amount)
        {
            return amount > 500;
        }
        public ApiResponseDtos<bool> MakePayment(PaymentModel model)
        {
            return RetryPolicy.GetPolicyAsync(3, 2)
                          .ExecuteAsync(async () =>
                               FakeApiCall()).Result;
        }

        private ApiResponseDtos<bool> FakeApiCall()
        {
            var random = new Random().Next(0, 10);

            if (random > 5)
            {
                throw new Exception("Payment Could not be Processed");
            }
            return new ApiResponseDtos<bool> { Payload = true, Code = ApiResponseCodes.OK };
        }
    }
}
