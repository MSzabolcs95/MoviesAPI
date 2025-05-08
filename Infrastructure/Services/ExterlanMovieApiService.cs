using Microsoft.Extensions.Options;
using MoviesAPI.Application.DTOs;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Infrastructure.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace MoviesAPI.Infrastructure.Services
{
    public class ExterlanMovieApiService : IExternalMovieApiService
    {
        private readonly RestClient _client;

        public ExterlanMovieApiService(IOptions<MovieApiOptions> options)
        {
            var movieApiOptions = options.Value;

            // Initialize RestClient with the base URL
            _client = new RestClient(movieApiOptions.BaseUrl);

            // Add default headers
            _client.AddDefaultHeader("Authorization", $"Bearer {movieApiOptions.BearerToken}");
            _client.AddDefaultHeader("accept", "application/json");
        }

        public async Task<List<MovieDto>> GetNowPlayingMoviesAsync(int pageNumber = 1)
        {
            return await FetchMoviesAsync($"/movie/now_playing?language=en-US&page={pageNumber}");
        }

        public async Task<List<MovieDto>> SearchMoviesAsync(string query, List<int> genreIds, int pageNumber = 1)
        {
            var encodedQuery = Uri.EscapeDataString(query);
            var movies = await FetchMoviesAsync($"/search/movie?query={encodedQuery}&language=en-US&page={pageNumber}");

            if (genreIds.Count == 0)
                return movies;

            return movies.Where(e => e.GenreIds.Any(id => genreIds.Contains(id))).ToList();
        }

        public async Task<List<MovieDto>> GetTopRatedMoviesAsync(int pageNumber = 1)
        {
            return await FetchMoviesAsync($"/movie/top_rated?language=en-US&page={pageNumber}");
        }

        public async Task<MovieDetailsResponse> GetDetailsAsync(int movieId)
        {
            var url = $"/movie/{movieId}?append_to_response=credits,images";
            return await FetchMovieDetailsAsync(url);
        }

        public async Task<List<GenreDto>> GetGenresAsync()
        {
            var url = "/genre/movie/list?language=en-US";
            return await FetchGenresAsync(url);
        }

        private async Task<List<MovieDto>> FetchMoviesAsync(string relativeUrl)
        {
            var request = new RestRequest(relativeUrl, Method.Get);

            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Request to {relativeUrl} failed with status code {response.StatusCode}.");
            }

            var movieResponse = JsonConvert.DeserializeObject<MovieApiResponse>(response.Content);

            if (movieResponse?.Results == null)
            {
                return new List<MovieDto>();
            }

            return movieResponse.Results.Select(movie => new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                PosterPath = movie.PosterPath,
                GenreIds = movie.GenreIds
            }).ToList();
        }

        private async Task<MovieDetailsResponse> FetchMovieDetailsAsync(string relativeUrl)
        {
            var request = new RestRequest(relativeUrl, Method.Get);

            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Request to {relativeUrl} failed with status code {response.StatusCode}.");
            }

            var movieDetails = JsonConvert.DeserializeObject<MovieDetailsResponse>(response.Content);

            if (movieDetails == null)
            {
                throw new InvalidOperationException("Failed to deserialize movie details response.");
            }

            return movieDetails;
        }

        private async Task<List<GenreDto>> FetchGenresAsync(string relativeUrl)
        {
            var request = new RestRequest(relativeUrl, Method.Get);

            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Request to {relativeUrl} failed with status code {response.StatusCode}.");
            }

            var genreResponse = JsonConvert.DeserializeObject<GenreApiResponse>(response.Content);

            if (genreResponse?.Genres == null)
            {
                return new List<GenreDto>();
            }

            return genreResponse.Genres;
        }
    }
}
