using ImageGallery.Api.Interfaces.Services;
using ImageGallery.Api.Models.Dtos;
using ImageGallery.Api.Services;
using Microsoft.Extensions.Caching.Memory;
using Shouldly;

namespace ImageGallery.Test.Services
{
    public class CacheServiceTests
    {
        ICacheService<UserImageDto> _cacheService;
        IMemoryCache _memoryCache;

        static readonly List<UserImageDto> mockUserImages = new List<UserImageDto>
        {
            new UserImageDto { Id = 1 },
            new UserImageDto { Id = 2 }
        };

        [SetUp]
        public void Setup()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());

            _cacheService = new CacheService<UserImageDto>(_memoryCache);
        }

        [TearDown]
        public void Teardown()
        {
            _memoryCache.Dispose();
        }

        [Test]
        public void DeleteItems()
        {
            _cacheService.SetItems("userimages", mockUserImages);

            var userImages = _cacheService.GetItems("userimages");
            userImages.ShouldBeOfType<List<UserImageDto>>();
            userImages.Count.ShouldBe(2);
            _cacheService.DeleteItems("userimages");
            var result = _cacheService.GetItems("userimages");

            result.ShouldBeNull();
        }

        [TestCase(null!)]
        [TestCase("")]
        [TestCase(" ")]
        public void DeleteItems_GivenInvalidKey_ShouldThrow_ArgumentException(string key)
        {
            Should.Throw<ArgumentException>(() => _cacheService.DeleteItems(key));
        }

        [Test]
        public void GetItems()
        {
            _cacheService.SetItems("userimages", mockUserImages);

            var result = _cacheService.GetItems("userimages");
            result.ShouldBeOfType<List<UserImageDto>>();
            result.Count.ShouldBe(2);
        }

        [TestCase(null!)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetItems_GivenInvalidKey_ShouldThrow_ArgumentException(string key)
        {
            Should.Throw<ArgumentException>(() => _cacheService.GetItems(key));
        }

        [Test]
        public void SetItems()
        {
            _cacheService.SetItems("userimages", mockUserImages);
            var result = _cacheService.GetItems("userimages");

            result.ShouldBeOfType<List<UserImageDto>>();
            result.Count.ShouldBe(2);
        }

        [Test]
        public void SetItems_GivenEmptyListOfItems_ShouldDoNothing()
        {
            _cacheService.SetItems("userimages", new List<UserImageDto>());
            var result = _cacheService.GetItems("userimages");

            result.ShouldBeNull();
        }

        [TestCase(null!)]
        [TestCase("")]
        [TestCase(" ")]
        public void SetItems_GivenInvalidKey_ShouldThrow_ArgumentException(string key)
        {
            Should.Throw<ArgumentException>(() => _cacheService.SetItems(key, new List<UserImageDto>()));
        }
    }
}
