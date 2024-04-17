using ImageGallery.Api.Controllers;
using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace ImageGallery.Test.Controllers
{
    public class VotesControllerTests
    {
        Mock<ILogger<VotesController>> _mockLogger;
        Mock<IVoteRepository> _mockVoteRepository;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<VotesController>>();

            _mockVoteRepository = new Mock<IVoteRepository>();
            _mockVoteRepository.Setup(v => v.AddVoteAsync(It.IsAny<VoteDto>()));
        }

        [Test]
        public async Task CreateVote()
        {
            var votesController = new VotesController(_mockLogger.Object, _mockVoteRepository.Object);

            var actionResult = await votesController.CreateVote(new VoteDto());
            var result = actionResult as StatusCodeResult;

            result.StatusCode.ShouldBe(StatusCodes.Status201Created);
        }

        [Test]
        public async Task CreateVote_GivenInvalidVote_ShouldReturn_BadRequest()
        {
            var votesController = new VotesController(_mockLogger.Object, _mockVoteRepository.Object);

            var actionResult = await votesController.CreateVote(null!);
            var result = actionResult as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

        [Test]
        public async Task CreateVote_Failure_ShouldReturn_InternalServerError()
        {
            _mockVoteRepository.Setup(v => v.AddVoteAsync(It.IsAny<VoteDto>())).Throws(new Exception());
            var votesController = new VotesController(_mockLogger.Object, _mockVoteRepository.Object);

            var actionResult = await votesController.CreateVote(new VoteDto());
            var result = actionResult as StatusCodeResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }
    }
}
