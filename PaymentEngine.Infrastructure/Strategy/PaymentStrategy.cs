using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Providers;
using PaymentEngine.Infrastructure.Providers.Interfaces;
using PaymentEngine.Infrastructure.Providers.Models;
using PaymentEngine.Infrastructure.Strategy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentEngine.Infrastructure.Strategy
{
    public class PaymentStrategy : IPaymentStrategy
    {
        private readonly IEnumerable<IPaymentGateway> _paymentGateways;
        public PaymentStrategy(IEnumerable<IPaymentGateway> paymentGateways)
        {
            if (paymentGateways == null) throw new ArgumentNullException("paymentGateways");
            _paymentGateways = paymentGateways;
        }

        public ApiResponseDtos<bool> ProcessPayment(PaymentModel paymentModel)
        {
            var provider = this._paymentGateways.FirstOrDefault(x => x.IsValid(paymentModel.Amount));
            if (provider == null)
            {
                provider = this._paymentGateways.FirstOrDefault(x => x.GetType().Name == typeof(CheapPaymentGateway).Name);
            }
            return provider.MakePayment(paymentModel);
        }
    }
}
