namespace MoviesAPI.Infrastructure.Configuration
{
    public class MovieApiOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string BearerToken { get; set; } = string.Empty;
    }
}