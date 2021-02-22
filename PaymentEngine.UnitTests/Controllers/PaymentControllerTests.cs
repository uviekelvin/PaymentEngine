using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PaymentEngine.Api.Controllers;
using PaymentEngine.Api.Mapper;
using PaymentEngine.Domain.Models;
using PaymentEngine.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.UnitTests.Controllers
{
    [TestFixture]
    public class PaymentControllerTests
    {
        private Mock<IPaymentService> paymentService;
        private IMapper mapper;
        [SetUp]
        public void SetUp()
        {
            paymentService = new Mock<IPaymentService>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProcessPaymentMapper());
            });

            mapper = mockMapper.CreateMapper();
        }

        [Test]
        public void ProcessPayment_IfPaymentFails_ShouldReturnBadRequest()
        {

            this.paymentService.Setup(c => c.ProcessPayment(It.IsAny<Payments>())).ReturnsAsync(
                new Infrastructure.Dtos.ApiResponseDtos<bool>
                {
                    Code = Infrastructure.Dtos.ApiResponseCodes.FAIL,
                    Message = "Payment Failed, Please try again"
                });

            var paymentController = new PaymentController(this.paymentService.Object, this.mapper);
            var result = paymentController.ProcessPayment(new Api.Dtos.ProcessPaymentDtos
            {
                Amount = 10,
                CardHolder = "UVIE KELVIN OGHENEJAKPOR",
                CreditCardNumber = "5399412001717771",
                ExpirationDate = DateTime.Now.Date,
                SecurityCode = "596",
            }).Result;

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void ProcessPayment_IfPaymentSuccessful_ShouldReturnSuccessful()
        {

            this.paymentService.Setup(c => c.ProcessPayment(It.IsAny<Payments>())).ReturnsAsync(
                new Infrastructure.Dtos.ApiResponseDtos<bool>
                {
                    Code = Infrastructure.Dtos.ApiResponseCodes.OK,
                    Message = "Payment Successful",
                    Payload = true
                });

            var paymentController = new PaymentController(this.paymentService.Object, this.mapper);
            var result = paymentController.ProcessPayment(new Api.Dtos.ProcessPaymentDtos
            {
                Amount = 10,
                CardHolder = "UVIE KELVIN OGHENEJAKPOR",
                CreditCardNumber = "5399412001717771",
                ExpirationDate = DateTime.Now.Date,
                SecurityCode = "596",
            }).Result;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}
