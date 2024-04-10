using ImageGallery.Api.Models.Common;

namespace ImageGallery.Api.Models.Dtos
{
    public class CommentDto : BaseModel
    {
        public string Content { get; set; } = string.Empty;
        public int UserImageId { get; set; }
        public UserImageDto? UserImage { get; set; }
    }
}
