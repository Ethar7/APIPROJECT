using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.BasketDtos;

namespace Ecommerence.Service
{
    public class PaymentService : IPaymentService
    {
        public Task<BasketDto> CreateOrUpdatePaymentIntend(string basketId)
        {
            throw new NotImplementedException();
        }
    }
}