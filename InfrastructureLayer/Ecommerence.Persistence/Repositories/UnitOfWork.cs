using System.Security.Cryptography;
using Ecommerence.Persistence.Data.DbContexts;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities;

namespace Ecommerence.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Dictionary<Type , object> _repositories = [];

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenaricRebository<TEntity, TKey> GetRebository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType, out var repository))
            {
                return (IGenaricRebository<TEntity, TKey>)repository;
            }
            var NewRepo = new GenaricRepository<TEntity , TKey>(_dbContext);

            _repositories[EntityType] = NewRepo;
            return NewRepo;
        }

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}