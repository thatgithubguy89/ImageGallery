using ImageGallery.Api.Models.Common;

namespace ImageGallery.Api.Models
{
    public class Comment : BaseModel
    {
        public string Content { get; set; } = string.Empty;
        public int UserImageId { get; set; }
        public UserImage? UserImage { get; set; }
    }
}
