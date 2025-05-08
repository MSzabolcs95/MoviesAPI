using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Application.Requests;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    // GET: api/Comment/movie/{movieId}
    [HttpGet("movie/{movieId:int}")]
    public async Task<IActionResult> GetAllCommentsForMovie(int movieId)
    {
        try
        {
            var comments = await _commentService.GetAllCommentsForMovie(movieId);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while fetching comments.");
        }
    }

    // GET: api/Comment/{commentId}
    [HttpGet("{commentId:int}")]
    public async Task<IActionResult> GetCommentById(int commentId)
    {
        var comment = await _commentService.GetCommentById(commentId);
        return Ok(comment);
    }

    // POST: api/Comment
    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] CommentRequest comment)
    {
        if (comment == null)
        {
            return BadRequest("Comment cannot be null");
        }
        try
        {
            var savedComment = await _commentService.AddComment(comment);
            return CreatedAtAction(nameof(GetCommentById), new { commentId = savedComment.Id }, savedComment);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while adding the comment.");
        }
    }
}
