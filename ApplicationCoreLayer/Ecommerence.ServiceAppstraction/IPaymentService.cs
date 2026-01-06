using Ecommerence.Shared.DTOS.BasketDtos;

namespace Ecommerence.ServiceAppstraction
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentIntend(string basketId);

    }
}