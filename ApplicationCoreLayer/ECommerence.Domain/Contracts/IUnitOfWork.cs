using ECommerence.Domain.Entities;

namespace ECommerence.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenaricRebository<TEntity, TKey> GetRebository<TEntity ,TKey>() where TEntity : BaseEntity<TKey>;
    }
}