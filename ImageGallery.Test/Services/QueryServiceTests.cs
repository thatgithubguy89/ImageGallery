using ImageGallery.Api.Interfaces.Services;
using ImageGallery.Api.Models.Dtos;
using ImageGallery.Api.Models.Requests;
using ImageGallery.Api.Services;
using Shouldly;

namespace ImageGallery.Test.Services
{
    public class QueryServiceTests
    {
        IQueryService<UserImageDto> _queryService;

        static readonly List<UserImageDto> mockUserImages = new List<UserImageDto>
        {
            new UserImageDto { Id = 1, Title = "test"},
            new UserImageDto { Id = 2, Title = "not"},
            new UserImageDto { Id = 3, Title = "test"},
            new UserImageDto { Id = 4, Title = "test2e332"},
            new UserImageDto { Id = 5, Title = "test"},
            new UserImageDto { Id = 6, Title = "testjlkjfdsjf;da"},
        };

        [SetUp]
        public void Setup()
        {
            _queryService = new QueryService<UserImageDto>();
        }

        [Test]
        public void RunQuery()
        {
            var queryRequest = new QueryRequest
            {
                Filter = new FilterRequest { Field = "Title", Value = "test" },
                Page = new PageRequest { StartingIndex = 1, PageTotal = 3 }
            };

            var result = _queryService.RunQuery(queryRequest, mockUserImages);

            result.Payload.ShouldBeOfType<List<UserImageDto>>();
            result.Payload.Count.ShouldBe(queryRequest.Page.PageTotal);
            result.StartingIndex.ShouldBe(queryRequest.Page.StartingIndex);
            result.Pages.ShouldBe(2);
        }

        [Test]
        public void RunQuery_GivenEmptyListOfItems_ResponsePayloadCount_ShouldBe_Zero()
        {
            var result = _queryService.RunQuery(new QueryRequest(), new List<UserImageDto>());

            result.Payload.ShouldBeOfType<List<UserImageDto>>();
            result.Payload.Count.ShouldBe(0);
            result.StartingIndex.ShouldBe(1);
            result.Pages.ShouldBe(1);
        }

        [Test]
        public void RunQuery_GivenInvalidRequest_ShouldThrow_ArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _queryService.RunQuery(null!, mockUserImages));
        }
    }
}
