using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;

namespace ImageGallery.Api.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment, CommentDto>
    {
        Task<List<CommentDto>> GetCommentsByUsernameAsync(string username);
    }
}
