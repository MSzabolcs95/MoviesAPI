using MoviesAPI.Application.DTOs;
using MoviesAPI.Application.Requests;
using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Application.Interfaces
{
    public interface ICommentService
    {
        public Task<List<CommentDto>> GetAllCommentsForMovie(int movieId);
        public Task<Comment> GetCommentById(int commentId);
        public Task<Comment> AddComment(CommentRequest comment);
    }
}
