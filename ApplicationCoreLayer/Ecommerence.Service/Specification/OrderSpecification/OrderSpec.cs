using System.Linq.Expressions;
using DomainLayer.Models.OrderModels;

namespace Ecommerence.Service.Specification.OrderSpecification
{
    public class OrderSpec : BaseSpecification<Order, Guid>
    {
        public OrderSpec(string email): base(o=>o.UserEmail== email)
        {
            AddInclude(o=> o.DeliveryMethod);
            AddInclude(o=>o.Items);
            AddOrderByDescending(o=> o.OrderDate);


        }

        public OrderSpec(Guid id): base(o=>o.Id== id)
        {
            AddInclude(o=> o.DeliveryMethod);
            AddInclude(o=>o.Items);  
        }
    }
}