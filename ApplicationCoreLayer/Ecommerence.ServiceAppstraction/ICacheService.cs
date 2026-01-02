namespace Ecommerence.ServiceAppstraction
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string cacheKey);
        Task SetAsync(string cacheKey, object value, TimeSpan timeToLive);
    }
}