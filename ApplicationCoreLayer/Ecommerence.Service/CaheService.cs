using System.Text.Json;
using Ecommerence.ServiceAppstraction;
using ECommerence.Domain.Contracts;

namespace Ecommerence.Service
{
    public class CaheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string cacheKey)
        {
            return await _cacheRepository.GetAsync(cacheKey);
        }

        public async Task SetAsync(string cacheKey, object value, TimeSpan timeToLive)
        {
            var valueToCache = JsonSerializer.Serialize(value);
            await _cacheRepository.SetAsync(cacheKey, valueToCache, timeToLive);
        }
    }
}