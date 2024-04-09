using ImageGallery.Api.Models.Common;

namespace ImageGallery.Api.Models
{
    public class UserImage : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int Views { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
