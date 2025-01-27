using BlogManagementAPI.Controllers.Base;
using BlogManagementDomain.Blog;
using BlogManagementService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : BaseController
    {
        #region Fields
        private readonly ICommentService _commentService;
        #endregion

        #region Constructor
        public CommentsController(ICommentService commentService, ILogger<CommentsController> logger)
            : base(logger)
        {
            _commentService = commentService;
        }
        #endregion

        #region Endpoints
        [HttpPost]
        public async Task<IActionResult> AddComment(int postId, [FromBody] CommentDomain comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try{
                _logger.LogInformation("Adding a new comment to post with ID: {PostId}", postId);
                comment.BlogPostId = postId;
                await _commentService.AddCommentAsync(comment);
                return CreatedAtAction(nameof(AddComment), new { postId, id = comment.Id }, comment);
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"Failed to add a comment to post with ID: {postId}");
            }
        }
                
        [HttpGet]
        public async Task<IActionResult> GetCommentsByPost(int postId)
        {
            try
            {
                _logger.LogInformation("Fetching comments for post with ID: {PostId}", postId);
                var comments = await _commentService.GetCommentsByPostIdAsync(postId);
                return Ok(comments.Select(c => new { c.Id, c.Content }));
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"Failed to fetch comments for post with ID: {postId}");
            }
        }

        #endregion
    }
}