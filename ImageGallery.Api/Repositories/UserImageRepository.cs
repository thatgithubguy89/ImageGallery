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
    }
}
