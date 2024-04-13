using AutoMapper;
using ImageGallery.Api.Data;
using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Api.Repositories
{
    public class UserImageRepository : Repository<UserImage, UserImageDto>, IUserImageRepository
    {
        private readonly ImageGalleryDbContext _context;

        public UserImageRepository(ImageGalleryDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public override async Task<List<UserImageDto>> GetAllAsync()
        {
            var userImages = await _context.UserImages.OrderByDescending(i => i.CreateTime)
                                                      .ToListAsync();

            return Convert(userImages);
        }

        public override async Task<UserImageDto> GetByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(id, 0);

            var userImage = await _context.UserImages.Include(i => i.Comments!.OrderByDescending(c => c.CreateTime))
                                                     .FirstOrDefaultAsync(i => i.Id == id);

            return Convert(userImage!);
        }

        public async Task<List<UserImageDto>> GetUserImagesByUsernameAsync(string username)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(username);

            var userImages = await _context.UserImages.Where(i => i.Username == username)
                                                      .OrderByDescending(i => i.CreateTime)
                                                      .ToListAsync();

            return Convert(userImages);
        }
    }
}
