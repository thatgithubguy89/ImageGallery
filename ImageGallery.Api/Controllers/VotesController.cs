using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly ILogger<VotesController> _logger;
        private readonly IVoteRepository _voteRepository;

        public VotesController(ILogger<VotesController> logger, IVoteRepository voteRepository)
        {
            _logger = logger;
            _voteRepository = voteRepository;
        }

        [HttpPost("createvote")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateVote(VoteDto voteDto)
        {
            try
            {
                _logger.LogInformation("Creating vote");

                if (voteDto == null)
                    return BadRequest();

                await _voteRepository.AddVoteAsync(voteDto);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create vote: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
