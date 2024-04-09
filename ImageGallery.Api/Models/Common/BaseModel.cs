namespace ImageGallery.Api.Models.Common
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
