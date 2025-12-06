

using System.Linq.Expressions;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities;

namespace Ecommerence.Service.Specification
{
    public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Include
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; } 
            = new List<Expression<Func<TEntity, object>>>();


        ICollection<Expression<Func<TEntity, object>>> ISpecification<TEntity, TKey>.IncludeExpression 
            => IncludeExpression;

        protected void AddInclude(Expression<Func<TEntity, object>> includeExp)
        {
            IncludeExpression.Add(includeExp);
        }
        #endregion
        #region where
        public Expression<Func<TEntity, bool>> Criteria {get;}
        

        public BaseSpecification(Expression<Func<TEntity, bool>> CriteriaExp)
        {
            Criteria = CriteriaExp;
        }
        #endregion
        

        #region Sorting
         public Expression<Func<TEntity, object>> OrderBy { get; private set;}

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set;}


        protected void AddOrderBy(Expression<Func<TEntity, object>> orderExp)
        {
            OrderBy = orderExp;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderExp)
        {
            OrderByDescending = orderExp;
        }

        #endregion
     
        #region Pagination
        public int Skip {get; private set;}

        public int Take {get; private set;}

        public bool IsPaginated {get; set;}

        protected void ApplyPagination(int pageSize , int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
        #endregion
    
    
    }
}
