using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserImagesController : ControllerBase
    {
        private readonly ILogger<UserImagesController> _logger;
        private readonly IUserImageRepository _userImageRepository;

        public UserImagesController(ILogger<UserImagesController> logger, IUserImageRepository userImageRepository)
        {
            _logger = logger;
            _userImageRepository = userImageRepository;
        }

        [HttpGet("getalluserimages")]
        [ProducesResponseType(typeof(List<UserImageDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllUserImages()
        {
            try
            {
                _logger.LogInformation("Getting all user images");

                return Ok(await _userImageRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get all user images: {}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
