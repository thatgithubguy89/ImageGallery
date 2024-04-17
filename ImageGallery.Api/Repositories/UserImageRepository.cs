using AutoMapper;
using ImageGallery.Api.Data;
using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Interfaces.Services;
using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Api.Repositories
{
    public class UserImageRepository : Repository<UserImage, UserImageDto>, IUserImageRepository
    {
        private readonly ICacheService<UserImage> _cacheService;
        private readonly ImageGalleryDbContext _context;

        public UserImageRepository(ICacheService<UserImage> cacheService, ImageGalleryDbContext context, IMapper mapper) : base(context, mapper)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public override async Task<UserImageDto> AddAsync(UserImageDto userImage)
        {
            ArgumentNullException.ThrowIfNull(userImage);

            var converted = Convert(userImage);

            var newUserImage = await _context.UserImages.AddAsync(converted);
            await _context.SaveChangesAsync();

            _cacheService.DeleteItems("alluserimages");
            _cacheService.DeleteItems($"{newUserImage.Entity.Username}-userimages");

            return Convert(newUserImage.Entity);
        }

        public override async Task<List<UserImageDto>> GetAllAsync()
        {
            var userImages = _cacheService.GetItems("alluserimages");
            if (userImages != null)
                return Convert(userImages);

            userImages = await _context.UserImages.OrderByDescending(i => i.CreateTime)
                                                  .ToListAsync();

            _cacheService.SetItems("alluserimages", userImages);

            return Convert(userImages);
        }

        public override async Task<UserImageDto> GetByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(id, 0);

            var userImage = await _context.UserImages.Include(i => i.Comments!.OrderByDescending(c => c.CreateTime))
                                                     .FirstOrDefaultAsync(i => i.Id == id);

            _context.Entry(userImage).State = EntityState.Detached;

            return Convert(userImage!);
        }

        public async Task<List<UserImageDto>> GetUserImagesByUsernameAsync(string username)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(username);

            var userImages = _cacheService.GetItems($"{username}-userimages");
            if (userImages != null)
                return Convert(userImages);

            userImages = await _context.UserImages.Where(i => i.Username == username)
                                                  .OrderByDescending(i => i.CreateTime)
                                                  .ToListAsync();

            _cacheService.SetItems($"{username}-userimages", userImages);

            return Convert(userImages);
        }

        public override async Task UpdateAsync(UserImageDto userImage)
        {
            await base.UpdateAsync(userImage);

            _cacheService.DeleteItems("alluserimages");
            _cacheService.DeleteItems($"{userImage.Username}-userimages");
        }
    }
}
