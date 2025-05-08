using MoviesAPI.Application.DTOs;
using MoviesAPI.Application.Requests;
using MoviesAPI.Infrastructure.Persistence;

namespace MoviesAPI.Application.Interfaces
{
    public interface IMovieService
    {
        Task<MovieDetailsResponse> GetDetailsAsync(int movieId);
        Task<List<MovieDto>> GetNowPlayingMoviesAsync(int pageNumber = 1);
        Task<List<MovieDto>> GetTopRatedMoviesAsync(int pageNumber = 1);
        Task<List<GenreDto>> GetGenres();
        Task<List<MovieDto>> SearchMoviesAsync(MovieSearchRequest movieSearchBody);
    }

}
