using MoviesAPI.Application.DTOs;
using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentDto>> GetAllCommentsForMovie(int movieId);
        Task AddComment(Comment comment);
        Task<Comment> GetCommentById(int commentId); 
    }
}
