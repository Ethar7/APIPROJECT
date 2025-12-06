
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

        public Expression<Func<TEntity, object>> OrderBy {get;}

        public Expression<Func<TEntity, object>> OrderByDescending {get;}

        public int Skip {get;}

        public int Take {get;}

        public bool IsPaginated {get;}
       
    }
}
