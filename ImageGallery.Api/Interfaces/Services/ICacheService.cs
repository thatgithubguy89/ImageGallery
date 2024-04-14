using Microsoft.Extensions.Caching.Memory;

namespace ImageGallery.Api.Interfaces.Services
{
    public interface ICacheService<T> where T : class
    {
        void DeleteItems(string key);
        List<T> GetItems(string key);
        void SetItems(string key, List<T> items, double slidingExp = 10, double absoluteExp = 30, CacheItemPriority priority = CacheItemPriority.Low);
    }
}
