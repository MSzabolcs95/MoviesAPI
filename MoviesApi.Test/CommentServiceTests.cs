using FluentAssertions;
using Moq;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Application.Services;
using MoviesAPI.Application.DTOs;
using MoviesAPI.Application.Requests;
using MoviesAPI.Domain.Entities;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace MoviesAPI.Tests
{
    public class CommentServiceTests
    {
        private readonly Mock<ICommentRepository> _mockCommentRepository;
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly CommentService _commentService;

        public CommentServiceTests()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            _mockUserManager = new Mock<UserManager<AppUser>>(
                Mock.Of<IUserStore<AppUser>>(),
                null, null, null, null, null, null, null, null
            );
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            _commentService = new CommentService(
                _mockCommentRepository.Object,
                _mockUserManager.Object,
                _mockHttpContextAccessor.Object
            );
        }

        [Fact]
        public async Task AddComment_ShouldSaveAndReturnComment_WhenValidRequest()
        {
            // Arrange  
            var commentRequest = new CommentRequest
            {
                Content = "Great movie!",
                MovieId = 1,
                UserId = "user123"
            };

            var savedComment = new Comment
            {
                Id = 1,
                Content = "Great movie!",
                MovieId = 1,
                UserId = "user123",
                CreatedAt = DateTime.UtcNow
            };

            _mockCommentRepository
                .Setup(static repo => repo.AddComment(It.IsAny<Comment>()))
                .Returns(Task.CompletedTask);

            // Act  
            var result = await _commentService.AddComment(commentRequest);

            // Assert  
            result.Should().NotBeNull();
            _mockCommentRepository.Verify(repo => repo.AddComment(It.IsAny<Comment>()), Times.Once);
        }

        [Fact]
        public async Task AddComment_ShouldThrowException_WhenRequestIsInvalid()
        {
            // Arrange
            var invalidRequest = new CommentRequest
            {
                Content = "", // Invalid: Empty content
                MovieId = 0,  // Invalid: MovieId must be greater than 0
                UserId = null // Invalid: UserId is null
            };

            // Act
            Func<Task> act = async () => await _commentService.AddComment(invalidRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
            _mockCommentRepository.Verify(repo => repo.AddComment(It.IsAny<Comment>()), Times.Never);
        }

        [Fact]
        public async Task GetAllCommentsForMovie_ShouldReturnComments_WhenMovieHasComments()
        {
            // Arrange
            var movieId = 1;
            var comments = new List<CommentDto>
    {
            new CommentDto { Id = 1, Content = "Great movie!", MovieId = movieId, Username = "user1" },
            new CommentDto { Id = 2, Content = "Loved it!", MovieId = movieId, Username = "user2" }
    };

            _mockCommentRepository
                .Setup(repo => repo.GetAllCommentsForMovie(movieId))
                .ReturnsAsync(comments);

            // Act
            var result = await _commentService.GetAllCommentsForMovie(movieId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Content.Should().Be("Great movie!");
            _mockCommentRepository.Verify(repo => repo.GetAllCommentsForMovie(movieId), Times.Once);
        }
    }
}