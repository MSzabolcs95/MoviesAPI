using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MoviesAPI.Application.DTOs;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    [JsonProperty("poster_path")]
    public string PosterPath { get; set; } = string.Empty;
    public string ReleaseDate { get; set; } = string.Empty;

    [JsonProperty("genre_ids")]
    public List<int> GenreIds { get; set; } = new List<int>();
    public string FullPosterPath => $"https://image.tmdb.org/t/p/w500{PosterPath}";
}