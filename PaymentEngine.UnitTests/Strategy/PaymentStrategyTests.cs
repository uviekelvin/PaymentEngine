using NUnit.Framework;
using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Providers;
using PaymentEngine.Infrastructure.Providers.Interfaces;
using PaymentEngine.Infrastructure.Strategy;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.UnitTests.Strategy
{
    [TestFixture]
    public class PaymentStrategyTests
    {

        [Test]
        public void CreatePaymentGateway_IfAmountBetweenZeroto200_UseCheapPaymentGatew()
        {

            var strategy = new PaymentStrategy(new IPaymentGateway[] { new ExpensivePaymentGateway(), new CheapPaymentGateway(),
                new PremiumPaymentService() });
            var response = strategy.ProcessPayment(new Infrastructure.Providers.Models.PaymentModel { Amount = 10 });
            Assert.That(response.Payload, Is.True);
        }
    }
}
