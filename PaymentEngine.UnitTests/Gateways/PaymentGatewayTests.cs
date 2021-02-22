using NUnit.Framework;
using PaymentEngine.Infrastructure.Providers;
using PaymentEngine.Infrastructure.Providers.Interfaces;
using PaymentEngine.Infrastructure.Providers.Models;
using System.Collections.Generic;
using System.Linq;

namespace PaymentEngine.UnitTests.Gateways
{
    [TestFixture]

    public class IPaymentGatewayTests
    {

        private List<IPaymentGateway> paymentGateways;

        [SetUp]
        public void SetUp()
        {
            paymentGateways = new List<IPaymentGateway>
            {
                new CheapPaymentGateway(),
                new ExpensivePaymentGateway(),
                new PremiumPaymentService()
            };
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(19)]
        [TestCase(19.9)]
        public void CreatePaymentGateway_IfAmountLessThan20_UseCheapPaymentGateway(decimal amount)
        {

            var paymentModel = new PaymentModel { Amount = amount };
            var gateWayUsed = paymentGateways.FirstOrDefault(x => x.IsValid(paymentModel.Amount));
            Assert.That(gateWayUsed, Is.TypeOf<CheapPaymentGateway>());
        }

        [Test]
        [TestCase(21)]
        [TestCase(70)]
        [TestCase(80)]
        [TestCase(90)]
        [TestCase(100)]
        [TestCase(200)]
        [TestCase(300)]
        [TestCase(400)]
        [TestCase(500)]
        public void CreatePaymentGateway_IfAmountBetween21to500_UseExpensiveGateway(decimal amount)
        {

            var paymentModel = new PaymentModel { Amount = amount };
            var gateWayUsed = paymentGateways.FirstOrDefault(x => x.IsValid(paymentModel.Amount));
            Assert.That(gateWayUsed, Is.TypeOf<ExpensivePaymentGateway>());
        }


        [Test]
        [TestCase(501)]
        [TestCase(10000)]
        public void CreatePaymentGateway_IfAmountGreaterThan500_UsePremiumGateway(decimal amount)
        {

            var paymentModel = new PaymentModel { Amount = amount };
            var gateWayUsed = paymentGateways.FirstOrDefault(x => x.IsValid(paymentModel.Amount));
            Assert.That(gateWayUsed, Is.TypeOf<PremiumPaymentService>());
        }
    }
}
