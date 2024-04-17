using AutoMapper;
using ImageGallery.Api.Data;
using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;
using ImageGallery.Api.Profiles;
using ImageGallery.Api.Repositories;
using ImageGallery.Test.Utitlity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace ImageGallery.Test.Repositories
{
    public class RepositoryTests
    {
        IRepository<UserImage, UserImageDto> _repository;
        ImageGalleryDbContext _context;
        DbContextOptions<ImageGalleryDbContext> _contextOptions;
        IMapper _mapper;
        MapperConfiguration _mapperConfiguration;
        IHttpContextAccessor _httpContextAccessor;

        static readonly UserImageDto mockUserImage = new UserImageDto { Id = 1, Title = "test" };

        static readonly List<UserImage> mockUserImages = new List<UserImage>
        {
            new UserImage { Id = 1 },
            new UserImage { Id = 2 }
        };

        [SetUp]
        public void Setup()
        {
            _httpContextAccessor = Generator.GenerateHttpContextAccessor();

            _contextOptions = new DbContextOptionsBuilder<ImageGalleryDbContext>()
                .UseInMemoryDatabase("RepositoryTests")
                .Options;

            _context = new ImageGalleryDbContext(_contextOptions, _httpContextAccessor);

            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = new Mapper(_mapperConfiguration);

            _repository = new Repository<UserImage, UserImageDto>(_context, _mapper);

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
            var result = await _repository.AddAsync(mockUserImage);

            result.ShouldBeOfType<UserImageDto>();
            result.Id.ShouldBe(mockUserImage.Id);
            result.Title.ShouldBe(mockUserImage.Title);
        }

        [Test]
        public async Task AddAsync_GiveInvalidUserImage_ShouldThrow_ArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _repository.AddAsync(null!));
        }

        [Test]
        public async Task DeleteAsync()
        {
            await _repository.AddAsync(mockUserImage);
            _context.ChangeTracker.Clear();

            await _repository.DeleteAsync(mockUserImage);
            var result = await _repository.GetByIdAsync(mockUserImage.Id);

            result.ShouldBeNull();
        }

        [Test]
        public async Task DeleteAsync_GiveInvalidUserImage_ShouldThrow_ArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _repository.DeleteAsync(null!));
        }

        [Test]
        public async Task GetAllAsync()
        {
            await _context.UserImages.AddRangeAsync(mockUserImages);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            result.ShouldBeOfType<List<UserImageDto>>();
            result.Count.ShouldBe(mockUserImages.Count);
        }

        [Test]
        public async Task GetByIdAsync()
        {
            await _repository.AddAsync(mockUserImage);

            var result = await _repository.GetByIdAsync(mockUserImage.Id);

            result.ShouldBeOfType<UserImageDto>();
            result.Id.ShouldBe(mockUserImage.Id);
            result.Title.ShouldBe(mockUserImage.Title);
        }

        [Test]
        public async Task GetByIdAsync_GiveIdForUserImageThatDoesNotExist_ShouldReturn_Null()
        {
            var result = await _repository.GetByIdAsync(mockUserImage.Id);

            result.ShouldBeNull();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetByIdAsync_GiveInvalidId_ShouldThrow_ArgumentOutOfRangeException(int id)
        {
            await Should.ThrowAsync<ArgumentOutOfRangeException>(async () => await _repository.GetByIdAsync(id));
        }

        [Test]
        public async Task UpdateAsync()
        {
            await _repository.AddAsync(mockUserImage);
            _context.ChangeTracker.Clear();
            mockUserImage.Title = "updatedtest";

            await _repository.UpdateAsync(mockUserImage);
            var result = await _repository.GetByIdAsync(mockUserImage.Id);

            result.ShouldBeOfType<UserImageDto>();
            result.Id.ShouldBe(mockUserImage.Id);
            result.Title.ShouldBe(mockUserImage.Title);

            mockUserImage.Title = "test";
        }

        [Test]
        public async Task UpdateAsync_GiveInvalidUserImage_ShouldThrow_ArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _repository.UpdateAsync(null!));
        }
    }
}
