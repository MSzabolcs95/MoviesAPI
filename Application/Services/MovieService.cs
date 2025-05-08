using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Application.DTOs;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Application.Requests;
using MoviesAPI.Infrastructure.Persistence;
using RestSharp;

public class MovieService : IMovieService
{
    private readonly IExternalMovieApiService _externalMovieApiService;

    public MovieService(IExternalMovieApiService externalMovieApiService)
    {
        _externalMovieApiService = externalMovieApiService;
    }

    public async Task<MovieDetailsResponse> GetDetailsAsync(int movieId)
    {
        return await _externalMovieApiService.GetDetailsAsync(movieId);
    }

    public async Task<List<MovieDto>> GetNowPlayingMoviesAsync(int pageNumber = 1)
    {
        var movies = await _externalMovieApiService.GetNowPlayingMoviesAsync(pageNumber);
        return movies;
    }

    public async Task<List<MovieDto>> GetTopRatedMoviesAsync(int pageNumber = 1)
    {
        var movies = await _externalMovieApiService.GetTopRatedMoviesAsync(pageNumber);
        return movies;
    }

    public async Task<List<GenreDto>> GetGenres()
    {
        return await _externalMovieApiService.GetGenresAsync();
    }

    public async Task<List<MovieDto>> SearchMoviesAsync(MovieSearchRequest movieSearchBody)
    {
        var movies = await _externalMovieApiService.SearchMoviesAsync(
            movieSearchBody.Query,
            movieSearchBody.GenreIds,
            movieSearchBody.PageNumber
        );
        return movies;
    }
}
