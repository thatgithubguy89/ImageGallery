using ImageGallery.Api.Models.Requests;
using ImageGallery.Api.Models.Responses;

namespace ImageGallery.Api.Interfaces.Services
{
    public interface IQueryService<T>
    {
        QueryResponse<T> RunQuery(QueryRequest request, List<T> items);
    }
}
