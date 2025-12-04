using Ecommerence.Persistence.Data.DbContexts;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecommerence.Persistence.Repositories
{
    public class GenaricRepository<TEntity, TKey> : IGenaricRebository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;
        public GenaricRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification)
        {
            // IQueryable<TEntity> Query = _dbContext.Set<TEntity>();

            // if (specification is not null)
            // {
            //     if (specification.IncludeExpression is not null && specification.IncludeExpression.Any())
            //     {
            //         foreach (var IncludeExp in specification.IncludeExpression)
            //         {
            //             Query = Query.Include(IncludeExp);
            //         }
            //     }
            // }
            // return await Query.ToListAsync();


            var Query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification);
            return await Query.ToListAsync();
           
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
            => await _dbContext.Set<TEntity>().FindAsync(id);


        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification).FirstOrDefaultAsync();
        }

        public void Remove(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);
    }
}