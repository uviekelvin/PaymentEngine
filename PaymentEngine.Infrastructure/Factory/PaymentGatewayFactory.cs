using PaymentEngine.Infrastructure.Providers;
using PaymentEngine.Infrastructure.Providers.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentEngine.Infrastructure.Factory
{
    public class PaymentGatewayFactory : IPaymentGatewayFactory
    {
        private readonly Dictionary<string, IPaymentGateway> _services
         = new Dictionary<string, IPaymentGateway>(StringComparer.OrdinalIgnoreCase);
        public PaymentGatewayFactory(IServiceProvider serviceProvider)
        {
            _services.Add(typeof(ExpensivePaymentGateway).Name, serviceProvider.GetService<ExpensivePaymentGateway>());
            _services.Add(typeof(CheapPaymentGateway).Name, serviceProvider.GetService<CheapPaymentGateway>());
            _services.Add(typeof(PremiumPaymentService).Name, serviceProvider.GetService<PremiumPaymentService>());
        }
        public IPaymentGateway Create(string input)
        {

            IPaymentGateway service = _services.ContainsKey(input) ? _services[input] : throw new Exception($"{input} not implemented");

            return service;
        }

    }
    public interface IPaymentGatewayFactory
    {
        IPaymentGateway Create(string input);
    }

}
