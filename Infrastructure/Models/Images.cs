using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MoviesAPI.Infrastructure.Models
{
    public class Images
    {
        [JsonProperty("backdrops")]
        public List<ImageItem> Backdrops { get; set; } = new List<ImageItem>();

        [JsonProperty("posters")]
        public List<ImageItem> Posters { get; set; } = new List<ImageItem>();
    }

    public class ImageItem
    {
        [JsonProperty("aspect_ratio")]
        public double AspectRatio { get; set; }

        [JsonProperty("file_path")]
        public string FilePath { get; set; } = string.Empty;

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("iso_639_1")]
        public string Iso6391 { get; set; } = string.Empty;

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}
