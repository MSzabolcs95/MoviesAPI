using MoviesAPI.Infrastructure.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MoviesAPI.Application.DTOs
{
    public class MovieDetailsResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("overview")]
        public string Overview { get; set; } = string.Empty;

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; } = string.Empty;

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        [JsonProperty("credits")]
        public Credits Credits { get; set; } = new Credits();

        [JsonProperty("images")]
        public Images Images { get; set; } = new Images();
    }
}
