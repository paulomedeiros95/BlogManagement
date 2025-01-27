using BlogManagementDomain.Blog;
using BlogManagementService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        #region Fields
        private readonly ICommentService _commentService;
        #endregion

        #region Constructor
        public CommentsController(ICommentService commentService)
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

            comment.BlogPostId = postId;
            await _commentService.AddCommentAsync(comment);
            return CreatedAtAction(nameof(AddComment), new { postId = comment.BlogPostId, id = comment.Id }, comment);
        }
                
        [HttpGet]
        public async Task<IActionResult> GetCommentsByPost(int postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            return Ok(comments.Select(c => new { c.Id, c.Content }));
        }

        #endregion
    }
}