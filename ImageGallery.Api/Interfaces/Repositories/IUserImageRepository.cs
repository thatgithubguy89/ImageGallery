using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;

namespace ImageGallery.Api.Interfaces.Repositories
{
    public interface IUserImageRepository : IRepository<UserImage, UserImageDto>
    {
    }
}
