using Microsoft.AspNetCore.Identity;
using MoviesAPI.Application.DTOs;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Application.Requests;
using MoviesAPI.Domain.Entities;
using System.Security.Claims;

namespace MoviesAPI.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(ICommentRepository commentRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CommentDto>> GetAllCommentsForMovie(int movieId)
        {
            if (movieId <= 0)
            {
                throw new ArgumentException("MovieId must be greater than 0.");
            }

            return await _commentRepository.GetAllCommentsForMovie(movieId);
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            var comment = await _commentRepository.GetCommentById(commentId);

            return comment;
        }

        public async Task<Comment> AddComment(CommentRequest comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                throw new ArgumentException("Comment content cannot be empty.");
            }

            if (comment.MovieId <= 0)
            {
                throw new ArgumentException("MovieId must be greater than 0.");
            }

            if (string.IsNullOrWhiteSpace(comment.UserId))
            {
                throw new ArgumentException("UserId cannot be null or empty.");
            }

            var CommentToSave = new Comment
            {
                CreatedAt = DateTime.UtcNow,
                UserId = comment.UserId,
                MovieId = comment.MovieId,
                Content = comment.Content
            };

            await _commentRepository.AddComment(CommentToSave);
            return CommentToSave;
        }

    }
}
