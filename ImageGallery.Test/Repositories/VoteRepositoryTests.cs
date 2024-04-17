using AutoMapper;
using ImageGallery.Api.Data;
using ImageGallery.Api.Models.Dtos;
using ImageGallery.Api.Models;
using ImageGallery.Api.Profiles;
using ImageGallery.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using ImageGallery.Api.Interfaces.Repositories;
using Shouldly;
using ImageGallery.Test.Utitlity;

namespace ImageGallery.Test.Repositories
{
    public class VoteRepositoryTests
    {
        IVoteRepository _voteRepository;
        Mock<IUserImageRepository> _mockUserImageRepository;
        ImageGalleryDbContext _context;
        DbContextOptions<ImageGalleryDbContext> _contextOptions;
        IMapper _mapper;
        MapperConfiguration _mapperConfiguration;
        IHttpContextAccessor _httpContextAccessor;

        static readonly VoteDto mockVote = new VoteDto { Id = 1, Like = true, UserImageId = 1, Username = "test@gmail.com" };

        static readonly UserImageDto mockUserImage = new UserImageDto { Id = 1, Username = "test@gmail.com" };

        [SetUp]
        public void Setup()
        {
            _httpContextAccessor = Generator.GenerateHttpContextAccessor();

            _mockUserImageRepository = new Mock<IUserImageRepository>();
            _mockUserImageRepository.Setup(i => i.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(mockUserImage));
            _mockUserImageRepository.Setup(i => i.UpdateAsync(It.IsAny<UserImageDto>()));

            _contextOptions = new DbContextOptionsBuilder<ImageGalleryDbContext>()
                .UseInMemoryDatabase("VoteRepositoryTests")
                .Options;

            _context = new ImageGalleryDbContext(_contextOptions, _httpContextAccessor);

            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = new Mapper(_mapperConfiguration);

            _voteRepository = new VoteRepository(_context, _httpContextAccessor, _mapper, _mockUserImageRepository.Object);

            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddVoteAsync()
        {
            await _context.UserImages.AddAsync(new UserImage { Id = 1, Username = "test@gmail.com" });
            await _context.SaveChangesAsync();

            var result = await _voteRepository.AddVoteAsync(mockVote);

            result.ShouldBe(true);
        }
    }
}
