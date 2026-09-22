using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TrendSense.Application.Interfaces;

namespace TrendSense.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<RedisCacheService> _logger;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger) => (_cache, _logger) = (cache, logger);

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var json = await _cache.GetAsync(key, cancellationToken);

                if (json is null)
                {
                    return default;
                }

                return JsonSerializer.Deserialize<T>(json, _jsonOptions);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get cache entry {@CacheKey}", key);

                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default)
        {
            try
            {
                var json = JsonSerializer.Serialize(value, _jsonOptions);

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                };

                await _cache.SetStringAsync(key, json, options, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to set cache entry {@CacheKey}", key);
            }
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cache.RemoveAsync(key, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to remove cache entry {@CacheKey}", key);
            }
        }
    }
}
