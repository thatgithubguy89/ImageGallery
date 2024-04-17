using AutoMapper;
using ImageGallery.Api.Data;
using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Api.Repositories
{
    public class VoteRepository : Repository<Vote, VoteDto>, IVoteRepository
    {
        private readonly ImageGalleryDbContext _context;
        private readonly IHttpContextAccessor _http;
        private readonly IUserImageRepository _userImageRepository;

        public VoteRepository(ImageGalleryDbContext context, IHttpContextAccessor http, IMapper mapper, IUserImageRepository userImageRepository) : base(context, mapper)
        {
            _context = context;
            _http = http;
            _userImageRepository = userImageRepository;
        }

        public async Task<bool> AddVoteAsync(VoteDto voteDto)
        {
            ArgumentNullException.ThrowIfNull(voteDto);

            if (await HasUserVotedAsync(voteDto))
                return false;

            await AddAsync(voteDto);

            await UpdateUserImageVoteCountAsync(voteDto);

            return true;
        }

        protected async Task<bool> HasUserVotedAsync(VoteDto voteDto)
        {
            var username = _http.HttpContext.User.Identity.Name;

            var vote = await _context.Votes.FirstOrDefaultAsync(v => v.UserImageId == voteDto.UserImageId && v.Username == username);

            if (vote != null)
                return true;

            return false;
        }

        protected async Task UpdateUserImageVoteCountAsync(VoteDto voteDto)
        {
            var userImage = await _userImageRepository.GetByIdAsync(voteDto.UserImageId);
            if (userImage == null)
                throw new NullReferenceException(nameof(userImage));


            if (voteDto.Like)
            {
                userImage.LikesCount++;
                await _userImageRepository.UpdateAsync(userImage);
                return;
            }

            userImage.DislikesCount++;
            await _userImageRepository.UpdateAsync(userImage);
        }
    }
}
