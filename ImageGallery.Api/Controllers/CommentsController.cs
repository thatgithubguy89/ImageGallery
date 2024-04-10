using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly IRepository<Comment, CommentDto> _commentRepository;

        public CommentsController(ILogger<CommentsController> logger, IRepository<Comment, CommentDto> commentRepository)
        {
            _logger = logger;
            _commentRepository = commentRepository;
        }

        [HttpPost("createcomment")]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateComment(CommentDto commentDto)
        {
            try
            {
                _logger.LogInformation("Creating comment");

                if (commentDto == null)
                    return BadRequest();

                var comment = await _commentRepository.AddAsync(commentDto);

                return CreatedAtAction(nameof(GetSingleComment), new { id = comment.Id }, comment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create comment: {}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("getsinglecomment/{id}")]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSingleComment(int id)
        {
            try
            {
                _logger.LogInformation("Getting comment with the id of {}", id);

                if (id <= 0)
                    return BadRequest();

                var comment = await _commentRepository.GetByIdAsync(id);
                if (comment == null)
                    return NotFound();

                return Ok(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get comment with the id of {}: {}", id, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
