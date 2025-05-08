using MoviesAPI.Infrastructure;

namespace MoviesAPI.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual AppUser User { get; set; } = null!;
    }
}
