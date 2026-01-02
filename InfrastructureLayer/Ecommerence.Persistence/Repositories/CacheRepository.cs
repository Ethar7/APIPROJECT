using ECommerence.Domain.Contracts;
using StackExchange.Redis;

namespace Ecommerence.Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string cacheKey)
        {
            var cachedValue = await _database.StringGetAsync(cacheKey);
            return cachedValue.IsNullOrEmpty ? null : cachedValue.ToString();
        }

        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan timeToLive)
        {
           await  _database.StringSetAsync(cacheKey, cacheValue, timeToLive);
        }
    }
}