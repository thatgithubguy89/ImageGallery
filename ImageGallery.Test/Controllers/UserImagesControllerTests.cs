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
    public class UserImagesControllerTests
    {
        Mock<IUserImageRepository> _mockUserImageRepository;
        Mock<ILogger<UserImagesController>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockUserImageRepository = new Mock<IUserImageRepository>();
            _mockUserImageRepository.Setup(i => i.GetAllAsync()).Returns(Task.FromResult(new List<UserImageDto>()));

            _mockLogger = new Mock<ILogger<UserImagesController>>();
        }

        [Test]
        public async Task GetAllUserImages()
        {
            var userImagesController = new UserImagesController(_mockLogger.Object, _mockUserImageRepository.Object);

            var actionResult = await userImagesController.GetAllUserImages();
            var result = actionResult as OkObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Value.ShouldBeOfType<List<UserImageDto>>();
        }

        [Test]
        public async Task GetAllUserImages_Failure_ShouldReturn_InternalServerError()
        {
            _mockUserImageRepository.Setup(i => i.GetAllAsync()).Throws(new Exception());
            var userImagesController = new UserImagesController(_mockLogger.Object, _mockUserImageRepository.Object);

            var actionResult = await userImagesController.GetAllUserImages();
            var result = actionResult as StatusCodeResult;

            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }
    }
}
