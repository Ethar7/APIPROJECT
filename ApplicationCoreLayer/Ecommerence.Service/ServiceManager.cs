using AutoMapper;
using Ecommerence.ServiceAppstraction;
using ECommerence.Domain.Contracts;

namespace Ecommerence.Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRebository) : IServiceManager
    {
        private readonly Lazy<IProductServices> _lazyProductservice =
            new Lazy<IProductServices>(() => new ProductService(_unitOfWork, _mapper));        
            public IProductServices ProductService => _lazyProductservice.Value;

         private readonly Lazy<IBasketService> _lazyBasketservice =
            new Lazy<IBasketService>(() => new BasketService(_basketRebository, _mapper));            
        
        public IBasketService BasketService => _lazyBasketservice.Value;
    }
}