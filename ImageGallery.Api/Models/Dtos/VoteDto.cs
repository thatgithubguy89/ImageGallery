using ImageGallery.Api.Models.Common;

namespace ImageGallery.Api.Models.Dtos
{
    public class VoteDto : BaseModel
    {
        public bool Like { get; set; }
        public bool Dislike { get; set; }
        public int UserImageId { get; set; }
        public UserImageDto? UserImage { get; set; }
    }
}
