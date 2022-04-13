using DevFreela.Core.DTO;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO);
    }
}
