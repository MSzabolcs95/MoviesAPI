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
            return await _commentRepository.GetAllCommentsForMovie(movieId);
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            var comment = await _commentRepository.GetCommentById(commentId);

            return comment;
        }

        public async Task<Comment> AddComment(CommentRequest comment)
        {
            var CommentToSave = new Comment();
            CommentToSave.CreatedAt = DateTime.UtcNow;

            // Use IHttpContextAccessor to get the current user's ID
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User is not authenticated.");
            }

            CommentToSave.UserId = userId;
            CommentToSave.MovieId = comment.MovieId;
            CommentToSave.Content = comment.Content;

            await _commentRepository.AddComment(CommentToSave);
            return CommentToSave;
        }
    }
}
