using ImageGallery.Api.Interfaces.Services;
using ImageGallery.Api.Models.Requests;
using ImageGallery.Api.Models.Responses;
using System.Linq.Dynamic.Core;

namespace ImageGallery.Api.Services
{
    public class QueryService<T> : IQueryService<T> where T : class
    {
        public QueryResponse<T> RunQuery(QueryRequest request, List<T> items)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (items.Count == 0)
            {
                return new QueryResponse<T>
                {
                    Payload = items,
                    StartingIndex = 1,
                    Pages = 1
                };
            }

            if (request.Filter != null)
                items = Filter(items, request.Filter.Field, request.Filter.Value);

            if (request.Page == null)
                return Page(items);

            return Page(items, request.Page.StartingIndex, request.Page.PageTotal);
        }

        // Returns entities where field is not null and contains the value
        protected List<T> Filter(List<T> items, string field, string value)
        {
            return items.AsQueryable()
                        .Where($"{field.ToLower()} != null && {field.ToLower()}.Contains(@0)", value)
                        .ToList();
        }

        protected QueryResponse<T> Page(List<T> items, int startingIndex = 1, float pageTotal = 12)
        {
            var pageCount = Math.Ceiling(items.Count / pageTotal);

            items = items.Skip((startingIndex - 1) * (int)pageTotal)
                         .Take((int)pageTotal)
                         .ToList();

            return new QueryResponse<T>
            {
                Payload = items,
                StartingIndex = startingIndex,
                Pages = (int)pageCount
            };
        }
    }
}
