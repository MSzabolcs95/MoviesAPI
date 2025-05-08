namespace MoviesAPI.Application.Requests
{
    public class MovieSearchRequest
    {
        public string Query { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}
