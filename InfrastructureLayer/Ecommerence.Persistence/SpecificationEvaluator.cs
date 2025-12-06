using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Ecommerence.Persistence
{
    public class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint , 
        ISpecification<TEntity , TKey> specification) where TEntity : BaseEntity<TKey>
        {
            var Query = EntryPoint;
            if (specification is not null)
            {

                if (specification.Criteria is not null)
                {
                    Query = Query.Where(specification.Criteria);
                }
                if (specification.IncludeExpression is not null && specification.IncludeExpression.Any())
                {
                    
                // foreach (var includeExp in specification.IncludeExpression)
                // {
                //     Query = Query.Include(includeExp);
                // }
            
                Query = specification.IncludeExpression.Aggregate(Query , (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
                }

                if (specification.OrderBy is not null)
                {
                    Query = Query.OrderBy(specification.OrderBy);
                }

                if (specification.OrderByDescending is not null)
                {
                    Query = Query.OrderByDescending(specification.OrderByDescending);
                }
            }
            return Query;

        }
    }
}