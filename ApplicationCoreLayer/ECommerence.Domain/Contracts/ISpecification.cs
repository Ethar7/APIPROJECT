
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ECommerence.Domain.Entities;

namespace ECommerence.Domain.Contracts
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        
        ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; }

        public Expression<Func<TEntity, bool>> Criteria {get;}

        public Expression<Func<TEntity, object>> OrderBy {get; set;}

        public Expression<Func<TEntity, object>> OrderByDescending {get; set;}

       
    }
}
