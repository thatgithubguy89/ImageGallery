namespace ImageGallery.Api.Models.Requests
{
    public class QueryRequest
    {
        public FilterRequest? Filter { get; set; }
        public PageRequest? Page { get; set; }
    }
}
