using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;

namespace ImageGallery.Api.Interfaces.Repositories
{
    public interface IVoteRepository : IRepository<Vote, VoteDto>
    {
        Task<bool> AddVoteAsync(VoteDto voteDto);
    }
}
