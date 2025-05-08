using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Application.Requests;

namespace MoviesAPI.Api.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("now-playing")]
        public async Task<IActionResult> Latest([FromQuery] int pageNumber = 1)
        {
            var movies = await _movieService.GetNowPlayingMoviesAsync(pageNumber);
            return Ok(movies);
        }

        [HttpGet("top-rated")]
        public async Task<IActionResult> GetTopRatedMovies([FromQuery] int pageNumber = 1)
        {
            var movies = await _movieService.GetTopRatedMoviesAsync(pageNumber);
            return Ok(movies);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetDetails([FromQuery] int movieId)
        {
            if (movieId <= 0)
                return BadRequest("Invalid movie ID.");
            var movieDetails = await _movieService.GetDetailsAsync(movieId);
            return Ok(movieDetails);
        }

        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _movieService.GetGenres();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] MovieSearchRequest movieSearchBody)
        {
            var movies = await _movieService.SearchMoviesAsync(movieSearchBody);
            return Ok(movies);
        } 
    }
}
