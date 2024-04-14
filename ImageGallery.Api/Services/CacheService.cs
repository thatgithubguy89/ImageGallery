using ImageGallery.Api.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace ImageGallery.Api.Services
{
    public class CacheService<T> : ICacheService<T> where T : class
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void DeleteItems(string key)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);

            _memoryCache.Remove(key);
        }

        public List<T> GetItems(string key)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);

            return _memoryCache.Get<List<T>>(key)!;
        }

        public void SetItems(string key, List<T> items, double slidingExp = 10, double absoluteExp = 30, CacheItemPriority priority = CacheItemPriority.Low)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);

            if (items.Count == 0)
                return;

            MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExp))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExp))
                .SetPriority(priority);

            _memoryCache.Set(key, items, memoryCacheEntryOptions);
        }
    }
}
