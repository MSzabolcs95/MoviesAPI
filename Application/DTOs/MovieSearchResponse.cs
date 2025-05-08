using MoviesAPI.Application.DTOs;
using MoviesAPI.Infrastructure.Persistence;

public class MovieSearchResponse
{
    public int Page { get; set; }
    public List<MovieDto> Results { get; set; }
    public int Total_Results { get; set; }
    public int Total_Pages { get; set; }
}