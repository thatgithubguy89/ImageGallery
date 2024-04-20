using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly ICommentRepository _commentRepository;

        public CommentsController(ILogger<CommentsController> logger, ICommentRepository commentRepository)
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
                _logger.LogError($"Failed to create comment: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpGet("getcommentsforuser/{username}")]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCommentsForUser(string username)
        {
            try
            {
                _logger.LogInformation($"Getting comments for user {username}");

                if (string.IsNullOrWhiteSpace(username))
                    return BadRequest();

                var comments = await _commentRepository.GetCommentsByUsernameAsync(username);

                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get comments for user {username}: {ex.Message}");

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
                _logger.LogInformation($"Getting comment with the id of {id}");

                if (id <= 0)
                    return BadRequest();

                var comment = await _commentRepository.GetByIdAsync(id);
                if (comment == null)
                    return NotFound();

                return Ok(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get comment with the id of {id}: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
