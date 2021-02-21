using PaymentEngine.Domain.Models;
using PaymentEngine.Infrastructure.Dtos;
using PaymentEngine.Infrastructure.Providers.Models;
using System.Threading.Tasks;

namespace PaymentEngine.Infrastructure.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<ApiResponseDtos<bool>> ProcessPayment(Payments model);
    }
}