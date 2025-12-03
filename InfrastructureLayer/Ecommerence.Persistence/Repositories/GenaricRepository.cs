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

        public async Task<TEntity?> GetByIdAsync(int id)
            => await _dbContext.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);
    }
}