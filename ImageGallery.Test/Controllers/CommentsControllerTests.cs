using ImageGallery.Api.Controllers;
using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace ImageGallery.Test.Controllers
{
    public class CommentsControllerTests
    {
        Mock<ICommentRepository> _mockCommentRepository;
        Mock<ILogger<CommentsController>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            _mockCommentRepository.Setup(c => c.AddAsync(It.IsAny<CommentDto>())).Returns(Task.FromResult(new CommentDto()));
            _mockCommentRepository.Setup(c => c.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new CommentDto()));

            _mockLogger = new Mock<ILogger<CommentsController>>();
        }

        [Test]
        public async Task GetSingleComment()
        {
            var commentsController = new CommentsController(_mockLogger.Object, _mockCommentRepository.Object);

            var actionResult = await commentsController.GetSingleComment(1);
            var result = actionResult as OkObjectResult;

            result.Value.ShouldBeOfType<CommentDto>();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetSingleComment_GivenInvalidId_ShouldReturn_BadRequest(int id)
        {
            var commentsController = new CommentsController(_mockLogger.Object, _mockCommentRepository.Object);

            var actionResult = await commentsController.GetSingleComment(id);
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task GetSingleComment_GivenIdForCommentThatDoesNotExist_ShouldReturn_NotFound()
        {
            _mockCommentRepository.Setup(c => c.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<CommentDto>(null!));
            var commentsController = new CommentsController(_mockLogger.Object, _mockCommentRepository.Object);

            var actionResult = await commentsController.GetSingleComment(1);
            var result = actionResult as NotFoundResult;

            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        }

        [Test]
        public async Task GetSingleComment_Failure_ShouldReturn_InternalServerErro()
        {
            _mockCommentRepository.Setup(c => c.GetByIdAsync(It.IsAny<int>())).Throws(new Exception());
            var commentsController = new CommentsController(_mockLogger.Object, _mockCommentRepository.Object);

            var actionResult = await commentsController.GetSingleComment(1);
            var result = actionResult as StatusCodeResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public async Task CreateComment()
        {
            var commentsController = new CommentsController(_mockLogger.Object, _mockCommentRepository.Object);

            var actionResult = await commentsController.CreateComment(new CommentDto());
            var result = actionResult as CreatedAtActionResult;

            result.Value.ShouldBeOfType<CommentDto>();
            result.StatusCode.ShouldBe(StatusCodes.Status201Created);
        }

        [Test]
        public async Task CreateComment_GivenInvalidComment_ShouldReturn_BadRequest()
        {
            var commentsController = new CommentsController(_mockLogger.Object, _mockCommentRepository.Object);

            var actionResult = await commentsController.CreateComment(null!);
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task CreateComment_Failure_ShouldReturn_InternalServerErro()
        {
            _mockCommentRepository.Setup(c => c.AddAsync(It.IsAny<CommentDto>())).Throws(new Exception());
            var commentsController = new CommentsController(_mockLogger.Object, _mockCommentRepository.Object);

            var actionResult = await commentsController.CreateComment(new CommentDto());
            var result = actionResult as StatusCodeResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }
    }
}
