using ImageGallery.Api.Models.Common;

namespace ImageGallery.Api.Models
{
    public class Vote : BaseModel
    {
        public bool Like { get; set; }
        public bool Dislike { get; set; }
        public int UserImageId { get; set; }
        public UserImage? UserImage { get; set; }
    }
}
