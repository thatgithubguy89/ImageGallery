using AutoMapper;
using ImageGallery.Api.Data;
using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models.Dtos;
using ImageGallery.Api.Models;
using ImageGallery.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using ImageGallery.Api.Profiles;
using Shouldly;

namespace ImageGallery.Test.Repositories
{
    public class UserImageRepositoryTests
    {
        IUserImageRepository _userImageRepository;
        ImageGalleryDbContext _context;
        DbContextOptions<ImageGalleryDbContext> _contextOptions;
        IMapper _mapper;
        MapperConfiguration _mapperConfiguration;

        static readonly List<Comment> mockComments = new List<Comment>
        {
            new Comment { Id = 1, UserImageId = 1, CreateTime = new DateTime(2023, 1, 1) },
            new Comment { Id = 2, UserImageId = 1, CreateTime = new DateTime(2024, 1, 1) },
        };

        static readonly UserImageDto mockUserImage = new UserImageDto { Id = 1 };

        static readonly List<UserImage> mockUserImages = new List<UserImage>
        {
            new UserImage { Id = 1, },
            new UserImage { Id = 2, }
        };

        [SetUp]
        public void Setup()
        {
            _contextOptions = new DbContextOptionsBuilder<ImageGalleryDbContext>()
                .UseInMemoryDatabase("UserImageRepositoryTests")
                .Options;

            _context = new ImageGalleryDbContext(_contextOptions);

            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = new Mapper(_mapperConfiguration);

            _userImageRepository = new UserImageRepository(_context, _mapper);

            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
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
            var username = "test@email.com";
            await _context.UserImages.AddRangeAsync(mockUserImages);
            await _context.SaveChangesAsync();

            var result = await _userImageRepository.GetUserImagesByUsernameAsync(username);

            result.ShouldBeOfType<List<UserImageDto>>();
            result.Count.ShouldBe(mockUserImages.Where(i => i.Username == username).ToList().Count);
        }

        [TestCase(null!)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task GetUserImagesByUsernameAsync_GivenInvalidUsername_ShouldThrow_ArgumentException(string username)
        {
            await Should.ThrowAsync<ArgumentException>(async () => await _userImageRepository.GetUserImagesByUsernameAsync(username));
        }
    }
}
