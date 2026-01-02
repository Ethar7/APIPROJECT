using Ecommerence.ServiceAppstraction;

namespace Ecommerence.Service
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductServices> ProductFactory,
                                                   Func<IBasketService> BasketFactory,
                                                   Func<IAuthunticationService> AuthFactory,
                                                   Func<IOrderService> OrderFactory) : IServiceManager
    {
        public IProductServices ProductService => ProductFactory.Invoke();

        public IBasketService BasketService => BasketFactory.Invoke();

        public IAuthunticationService AuthunticationService => AuthFactory.Invoke();

        public IOrderService orderService => OrderFactory.Invoke();
    }

}