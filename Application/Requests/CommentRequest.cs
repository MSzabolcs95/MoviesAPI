using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Application.Requests
{
    public class CommentRequest
    {
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Content { get; set; }

        [Range(1, int.MaxValue)]
        public int MovieId { get; set; }
    }
}
