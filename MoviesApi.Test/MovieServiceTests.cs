using FluentAssertions;
using Moq;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Application.Services;
using MoviesAPI.Application.DTOs;
using Xunit;
using MoviesAPI.Application.Requests;

namespace MoviesAPI.Tests
{
    public class MovieServiceTests
    {
        private readonly Mock<IExternalMovieApiService> _mockExternalMovieApiService;
        private readonly MovieService _movieService;

        public MovieServiceTests()
        {
            _mockExternalMovieApiService = new Mock<IExternalMovieApiService>();
            _movieService = new MovieService(_mockExternalMovieApiService.Object);
        }

        [Fact]
        public async Task GetNowPlayingMoviesAsync_ShouldReturnMovies_WhenApiReturnsData()
        {
            // Arrange
            var mockMovies = new List<MovieDto>
        {
            new MovieDto { Id = 1, Title = "Movie 1" },
            new MovieDto { Id = 2, Title = "Movie 2" }
        };

            _mockExternalMovieApiService
                .Setup(service => service.GetNowPlayingMoviesAsync(It.IsAny<int>()))
                .ReturnsAsync(mockMovies);

            // Act
            var result = await _movieService.GetNowPlayingMoviesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(m => m.Title == "Movie 1");
        }

        [Fact]
        public async Task SearchMoviesAsync_ShouldReturnFilteredMovies_WhenApiReturnsData()
        {
            // Arrange
            var mockMovies = new List<MovieDto>
    {
        new MovieDto { Id = 1, Title = "Action Movie", GenreIds = new List<int> { 1 } },
        new MovieDto { Id = 2, Title = "Comedy Movie", GenreIds = new List<int> { 2 } }
    };

            var searchRequest = new MovieSearchRequest
            {
                Query = "Action",
                GenreIds = new List<int> { 1, 2 },
                PageNumber = 1
            };

            _mockExternalMovieApiService
                .Setup(service => service.SearchMoviesAsync(It.IsAny<string>(), It.IsAny<List<int>>(), It.IsAny<int>()))
                .ReturnsAsync(mockMovies);

            // Act
            var result = await _movieService.SearchMoviesAsync(searchRequest);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Title.Should().Be("Action Movie");
        }

        [Fact]
        public async Task GetNowPlayingMoviesAsync_ShouldThrowException_WhenApiFails()
        {
            // Arrange
            _mockExternalMovieApiService
                .Setup(service => service.GetNowPlayingMoviesAsync(It.IsAny<int>()))
                .ThrowsAsync(new HttpRequestException("API error"));

            // Act
            Func<Task> act = async () => await _movieService.GetNowPlayingMoviesAsync();

            // Assert
            await act.Should().ThrowAsync<HttpRequestException>().WithMessage("API error");
        }
    }
}
