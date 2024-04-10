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

        [HttpGet("getsingleuserimage/{id}")]
        [ProducesResponseType(typeof(UserImageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSingleUserImage(int id)
        {
            try
            {
                _logger.LogInformation("Getting user image with the id of {}", id);

                if (id <= 0)
                    return BadRequest();

                var userImage = await _userImageRepository.GetByIdAsync(id);
                if (userImage == null)
                    return NotFound();

                return Ok(userImage);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get user image with the id of {}: {}", id, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
