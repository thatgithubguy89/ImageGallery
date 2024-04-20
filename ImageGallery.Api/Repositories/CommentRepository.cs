using AutoMapper;
using ImageGallery.Api.Data;
using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Api.Repositories
{
    public class CommentRepository : Repository<Comment, CommentDto>, ICommentRepository
    {
        private readonly ImageGalleryDbContext _context;

        public CommentRepository(ImageGalleryDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public async Task<List<CommentDto>> GetCommentsByUsernameAsync(string username)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(username);

            var comments = await _context.Comments.Where(c => c.Username == username)
                                                  .OrderByDescending(c => c.CreateTime)
                                                  .ToListAsync();

            return Convert(comments);
        }
    }
}
