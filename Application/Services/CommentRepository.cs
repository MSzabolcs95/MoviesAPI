using Microsoft.EntityFrameworkCore;
using MoviesAPI.Application.DTOs;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Infrastructure.Persistence;

namespace MoviesAPI.Application.Services
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public Task<List<CommentDto>> GetAllCommentsForMovie(int movieId)
        {
            return _context.Comments
                .Where(e => e.MovieId == movieId)
                .Select(e => new CommentDto
                {
                    Id = e.Id,
                    Content = e.Content,
                    CreatedAt = e.CreatedAt,
                    MovieId = e.MovieId,
                    Username = e.User.UserName ?? string.Empty
                })
                .ToListAsync();
        }


        public async Task<Comment> GetCommentById(int commentId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(e => e.Id == commentId);
            if (comment == null)
            {
                throw new InvalidOperationException($"Comment with ID {commentId} not found.");
            }
            return comment;
        }
    }
}

