// using AutoMapper;
// using Ecommerence.ServiceAppstraction;
// using ECommerence.Domain.Contracts;
// using ECommerence.Domain.Entities.IdentityModules;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Identity.Client;

// namespace Ecommerence.Service
// {
//     public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRebository, UserManager<ApplicationUser> _userManager, IConfiguration _configuration) 
//     // IServiceManager
//     {
//         private readonly Lazy<IProductServices> _lazyProductservice =
//             new Lazy<IProductServices>(() => new ProductService(_unitOfWork, _mapper));        
//             public IProductServices ProductService => _lazyProductservice.Value;

//          private readonly Lazy<IBasketService> _lazyBasketservice =
//             new Lazy<IBasketService>(() => new BasketService(_basketRebository, _mapper));            
        
//         public IBasketService BasketService => _lazyBasketservice.Value;

//         private readonly Lazy<IAuthunticationService> _lazyAuthenticationService= 
//         new Lazy<IAuthunticationService> (()=> new AuthunticationService(_userManager, _configuration, _mapper));

//         public IAuthunticationService AuthunticationService => _lazyAuthenticationService.Value;

//         private readonly Lazy<IOrderService> _lazyOrderService= 
//         new Lazy<IOrderService> (()=> new OrderService(_mapper, _basketRebository, _unitOfWork));


//         public IOrderService orderService => _lazyOrderService.Value;
//     }
// }