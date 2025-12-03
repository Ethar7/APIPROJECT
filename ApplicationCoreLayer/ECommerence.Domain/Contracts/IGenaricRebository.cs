using ECommerence.Domain.Entities;

namespace ECommerence.Domain.Contracts
{
    public interface IGenaricRebository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
    
}