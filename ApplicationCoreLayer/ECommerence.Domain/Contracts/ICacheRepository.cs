namespace ECommerence.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string cacheKey);

        Task SetAsync(string cacheKey, string cacheValue, TimeSpan timeToLive);

        
    }
}