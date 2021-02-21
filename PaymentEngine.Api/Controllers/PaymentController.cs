using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentEngine.Api.Dtos;
using PaymentEngine.Domain.Models;
using PaymentEngine.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentEngine.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;

        public PaymentController(IPaymentService paymentService, IMapper mapper)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentDtos processPayment)
        {
            var payment = this.mapper.Map<Payments>(processPayment);
            var paymentResponse = await this.paymentService.ProcessPayment(payment);
            return CustomResponse(paymentResponse);
        }
    }
}
