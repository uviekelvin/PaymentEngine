using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Providers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.Providers.Interfaces
{
    public interface IPaymentGateway
    {
        ApiResponseDtos<bool> MakePayment(PaymentModel model);
        bool IsValid(decimal amount);
    }
}
