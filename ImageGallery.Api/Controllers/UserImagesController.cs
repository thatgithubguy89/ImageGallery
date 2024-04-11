﻿using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Interfaces.Services;
using ImageGallery.Api.Models.Dtos;
using ImageGallery.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserImagesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILogger<UserImagesController> _logger;
        private readonly IUserImageRepository _userImageRepository;

        public UserImagesController(IFileService fileService, ILogger<UserImagesController> logger, IUserImageRepository userImageRepository)
        {
            _fileService = fileService;
            _logger = logger;
            _userImageRepository = userImageRepository;
        }

        [HttpPost("createuserimage")]
        [ProducesResponseType(typeof(UserImageDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUserImage([FromForm] AddUserImageRequest request)
        {
            try
            {
                _logger.LogInformation("Creating user image");

                if (request == null)
                    return BadRequest();

                request.UserImageDto.ImagePath = await _fileService.UploadFileAsync("images", request.File);
                var userimage = await _userImageRepository.AddAsync(request.UserImageDto);

                return CreatedAtAction(nameof(GetSingleUserImage), new { id = userimage.Id }, userimage);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create user image: {}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
