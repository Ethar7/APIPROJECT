using ECommerence.Domain.Entities;

namespace ECommerence.Domain.Contracts
{
    public interface IGenaricRebository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specification);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        Task<int> CountAsync(ISpecification<TEntity, TKey> specification);
    }
    
}