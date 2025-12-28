namespace Ecommerence.ServiceAppstraction
{
    public interface IServiceManager
    {
        public IProductServices ProductService {get;}

        public IBasketService BasketService{get;}

        public IAuthunticationService AuthunticationService{get;}
    }
}