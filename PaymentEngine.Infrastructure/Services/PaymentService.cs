using PaymentEngine.Domain.Models;
using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Factory;
using PaymentEngine.Infrastructure.Providers.Interfaces;
using PaymentEngine.Infrastructure.Providers.Models;
using PaymentEngine.Infrastructure.Repositories.Interfaces;
using PaymentEngine.Infrastructure.Services.Interfaces;
using PaymentEngine.Infrastructure.Strategy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {

        private readonly IPaymentStrategy paymentStrategy;
        private readonly IUnitofWork unitofWork;

        public PaymentService(IPaymentStrategy paymentStrategy, IUnitofWork unitofWork)
        {

            this.paymentStrategy = paymentStrategy;
            this.unitofWork = unitofWork;
        }

        public async Task<ApiResponseDtos<bool>> ProcessPayment(Payments model)
        {
            this.unitofWork.Repository<Payments>().Add(model);
            await this.unitofWork.Complete();
            var response = paymentStrategy.ProcessPayment(new PaymentModel
            {
                Amount = model.Amount,
                CreditCardNumber = model.CreditCardNumber,
                SecurityCode = model.SecurityCode,
                ExpirationDate = model.ExpirationDate
            });
            model.UpdatePaymentState(response != null && response.Code == ApiResponseCodes.OK);
            await this.unitofWork.Complete();
            return response;
        }
    }
}
