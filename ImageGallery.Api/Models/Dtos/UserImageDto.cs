using ImageGallery.Api.Models.Common;

namespace ImageGallery.Api.Models.Dtos
{
    public class UserImageDto : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int Views { get; set; }
        public ICollection<CommentDto>? Comments { get; set; }
    }
}
