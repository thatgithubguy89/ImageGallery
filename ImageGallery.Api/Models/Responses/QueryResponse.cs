namespace ImageGallery.Api.Models.Responses
{
    public class QueryResponse<T>
    {
        public List<T> Payload { get; set; }
        public int StartingIndex { get; set; }
        public int Pages { get; set; }
    }
}
