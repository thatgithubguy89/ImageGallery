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

        static readonly List<UserImage> mockUserImages = new List<UserImage>
        {
            new UserImage { Id = 1 },
            new UserImage { Id = 2 }
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
    }
}
