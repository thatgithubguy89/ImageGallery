using ImageGallery.Api.Models.Dtos;

namespace ImageGallery.Api.Models.Requests
{
    public class AddUserImageRequest
    {
        public UserImageDto UserImageDto { get; set; }
        public IFormFile File { get; set; }
    }
}
