using MoviesAPI.Application.DTOs;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Infrastructure.Persistence;

namespace MoviesAPI.Application.Interfaces
{
    public interface IExternalMovieApiService
    {
        Task<List<MovieDto>> GetNowPlayingMoviesAsync(int pageNumber = 1);
        Task<List<MovieDto>> SearchMoviesAsync(string query, List<int> genreIds, int pageNumber = 1);
        Task<List<MovieDto>> GetTopRatedMoviesAsync(int pageNumber = 1);
        Task<MovieDetailsResponse> GetDetailsAsync(int movieId);
        Task<List<GenreDto>> GetGenresAsync();
    }
}
