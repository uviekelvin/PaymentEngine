using Moq;
using NUnit.Framework;
using PaymentEngine.Domain.Models;
using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Providers.Models;
using PaymentEngine.Infrastructure.Repositories.Interfaces;
using PaymentEngine.Infrastructure.Services;
using PaymentEngine.Infrastructure.Strategy.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.UnitTests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {

        private Mock<IPaymentStrategy> payMentStrategy;
        private Mock<IUnitofWork> unitOfWork;
        [SetUp]
        public void SetUp()
        {
            payMentStrategy = new Mock<IPaymentStrategy>();
            unitOfWork = new Mock<IUnitofWork>();
        }

        [Test]
        public void ProcessPayment_IFPaymentGatewayReturnsSuccessful_TransactionSuccessful()
        {
            var payments = new Payments
            {
                Amount = 10,
                CardHolder = "UVIE KELVIN OGHENEJAKPOR",
                CreditCardNumber = "5399412001717771",
                ExpirationDate = DateTime.Now.Date,
                SecurityCode = "596",
                Id = 1
            };
            this.unitOfWork.Setup(c => c.Repository<Payments>().Add(payments));
            this.payMentStrategy.Setup(c => c.ProcessPayment(It.IsAny<PaymentModel>())).Returns(new Infrastructure.Dtos.ApiResponseDtos<bool> { Code = Infrastructure.Dtos.ApiResponseCodes.OK, Payload = true });
            var paymentService = new PaymentService(payMentStrategy.Object, this.unitOfWork.Object);
            var processPayment = paymentService.ProcessPayment(payments).Result;
            this.unitOfWork.Verify(c => c.Repository<Payments>().Add(payments));
            Assert.That(processPayment.Payload, Is.True);
        }

        [Test]
        public void ProcessPayment_IFPaymentGatewayReturnsFails_TransactionFailed()
        {
            var payments = new Payments
            {
                Amount = 10,
                CardHolder = "UVIE KELVIN OGHENEJAKPOR",
                CreditCardNumber = "5399412001717771",
                ExpirationDate = DateTime.Now.Date,
                SecurityCode = "596",
                Id = 1
            };
            this.unitOfWork.Setup(c => c.Repository<Payments>().Add(payments));
            this.payMentStrategy.Setup(c => c.ProcessPayment(It.IsAny<PaymentModel>())).Returns(new Infrastructure.Dtos.ApiResponseDtos<bool> { Code = Infrastructure.Dtos.ApiResponseCodes.FAIL, Payload = false });
            var paymentService = new PaymentService(payMentStrategy.Object, this.unitOfWork.Object);
            var processPayment = paymentService.ProcessPayment(payments).Result;
            this.unitOfWork.Verify(c => c.Repository<Payments>().Add(payments));
            Assert.That(processPayment.Payload, Is.False);
            Assert.That(processPayment.Code, Is.EqualTo(ApiResponseCodes.FAIL));
        }
    }
}
