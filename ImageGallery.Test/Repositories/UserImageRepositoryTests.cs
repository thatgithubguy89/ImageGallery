using AutoMapper;
using ImageGallery.Api.Data;
using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models.Dtos;
using ImageGallery.Api.Models;
using ImageGallery.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using ImageGallery.Api.Profiles;
using Shouldly;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;
using Moq;
using ImageGallery.Api.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace ImageGallery.Test.Repositories
{
    public class UserImageRepositoryTests
    {
        IUserImageRepository _userImageRepository;
        ImageGalleryDbContext _context;
        DbContextOptions<ImageGalleryDbContext> _contextOptions;
        IMapper _mapper;
        MapperConfiguration _mapperConfiguration;
        IHttpContextAccessor _httpContextAccessor;
        Mock<ICacheService<UserImage>> _mockCacheService;

        static readonly List<Comment> mockComments = new List<Comment>
        {
            new Comment { Id = 1, UserImageId = 1, CreateTime = new DateTime(2023, 1, 1) },
            new Comment { Id = 2, UserImageId = 1, CreateTime = new DateTime(2024, 1, 1) },
        };

        static readonly UserImageDto mockUserImage = new UserImageDto { Id = 1, Username = "test@gmail.com" };

        static readonly List<UserImage> mockUserImages = new List<UserImage>
        {
            new UserImage { Id = 1, },
            new UserImage { Id = 2, }
        };

        [SetUp]
        public void Setup()
        {
            var identity = new GenericIdentity("test@gmail.com", "test");
            var contextUser = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext();
            httpContext.User = contextUser;
            _httpContextAccessor = new HttpContextAccessor { HttpContext = httpContext };

            _contextOptions = new DbContextOptionsBuilder<ImageGalleryDbContext>()
                .UseInMemoryDatabase("UserImageRepositoryTests")
                .Options;

            _context = new ImageGalleryDbContext(_contextOptions, _httpContextAccessor);

            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = new Mapper(_mapperConfiguration);

            _mockCacheService = new Mock<ICacheService<UserImage>>();
            _mockCacheService.Setup(c => c.GetItems(It.IsAny<string>())).Returns<List<UserImageDto>>(null!);

            _userImageRepository = new UserImageRepository(_mockCacheService.Object, _context, _mapper);

            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddAsync()
        {
            var result = await _userImageRepository.AddAsync(mockUserImage);

            result.ShouldBeOfType<UserImageDto>();
            result.Id.ShouldBe(mockUserImage.Id);
            result.Title.ShouldBe(mockUserImage.Title);
            _mockCacheService.Verify(c => c.DeleteItems("alluserimages"), Times.Once);
            _mockCacheService.Verify(c => c.DeleteItems($"{mockUserImage.Username}-userimages"), Times.Once);
        }

        [Test]
        public async Task AddAsync_GiveInvalidUserImage_ShouldThrow_ArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _userImageRepository.AddAsync(null!));
        }

        [Test]
        public async Task GetAllAsync()
        {
            await _context.UserImages.AddRangeAsync(mockUserImages);
            await _context.SaveChangesAsync();

            var result = await _userImageRepository.GetAllAsync();

            result.ShouldBeOfType<List<UserImageDto>>();
            result.Count.ShouldBe(mockUserImages.Count);
        }

        [Test]
        public async Task GetAllAsync_CacheFound()
        {
            _mockCacheService.Setup(c => c.GetItems(It.IsAny<string>())).Returns(mockUserImages);
            await _context.UserImages.AddRangeAsync(mockUserImages);
            await _context.SaveChangesAsync();

            var result = await _userImageRepository.GetAllAsync();

            result.ShouldBeOfType<List<UserImageDto>>();
            result.Count.ShouldBe(mockUserImages.Count);
            _mockCacheService.Verify(c => c.SetItems(It.IsAny<string>(), It.IsAny<List<UserImage>>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<CacheItemPriority>()), Times.Never());
        }

        [Test]
        public async Task GetByIdAsync()
        {
            await _context.Comments.AddRangeAsync(mockComments);
            await _userImageRepository.AddAsync(mockUserImage);

            var result = await _userImageRepository.GetByIdAsync(mockUserImage.Id);

            result.ShouldBeOfType<UserImageDto>();
            result.Id.ShouldBe(mockUserImage.Id);
            result.Comments.Count.ShouldBe(mockComments.Count);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetByIdAsync_GivenInvalidId_ShouldThrow_ArgumentOutOfRangeException(int id)
        {
            await Should.ThrowAsync<ArgumentOutOfRangeException>(async () => await _userImageRepository.GetByIdAsync(id));
        }

        [Test]
        public async Task GetUserImagesByUsernameAsync()
        {
            var username = "test@gmail.com";
            await _context.UserImages.AddRangeAsync(mockUserImages);
            await _context.SaveChangesAsync();

            var result = await _userImageRepository.GetUserImagesByUsernameAsync(username);

            result.ShouldBeOfType<List<UserImageDto>>();
            result.Count.ShouldBe(mockUserImages.Where(i => i.Username == username).ToList().Count);
        }

        [Test]
        public async Task GetUserImagesByUsernameAsync_CacheFound()
        {
            _mockCacheService.Setup(c => c.GetItems(It.IsAny<string>())).Returns(mockUserImages);
            var username = "test@gmail.com";
            await _context.UserImages.AddRangeAsync(mockUserImages);
            await _context.SaveChangesAsync();

            var result = await _userImageRepository.GetUserImagesByUsernameAsync(username);

            result.ShouldBeOfType<List<UserImageDto>>();
            result.Count.ShouldBe(mockUserImages.Where(i => i.Username == username).ToList().Count);
            _mockCacheService.Verify(c => c.SetItems(It.IsAny<string>(), It.IsAny<List<UserImage>>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<CacheItemPriority>()), Times.Never());
        }

        [TestCase(null!)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task GetUserImagesByUsernameAsync_GivenInvalidUsername_ShouldThrow_ArgumentException(string username)
        {
            await Should.ThrowAsync<ArgumentException>(async () => await _userImageRepository.GetUserImagesByUsernameAsync(username));
        }

        [Test]
        public async Task UpdateAsync()
        {
            await _context.UserImages.AddAsync(_mapper.Map<UserImage>(mockUserImage));
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            mockUserImage.Title = "updatedtest";

            await _userImageRepository.UpdateAsync(mockUserImage);
            var result = await _userImageRepository.GetByIdAsync(mockUserImage.Id);

            result.ShouldBeOfType<UserImageDto>();
            result.Id.ShouldBe(mockUserImage.Id);
            result.Title.ShouldBe(mockUserImage.Title);
            _mockCacheService.Verify(c => c.DeleteItems("alluserimages"), Times.Once);
            _mockCacheService.Verify(c => c.DeleteItems($"{mockUserImage.Username}-userimages"), Times.Once);

            mockUserImage.Title = "test";
        }

        [Test]
        public async Task UpdateAsync_GiveInvalidUserImage_ShouldThrow_ArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _userImageRepository.UpdateAsync(null!));
        }
    }
}
