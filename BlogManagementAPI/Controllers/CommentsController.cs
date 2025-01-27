using AutoMapper;
using BlogManagementAPI.Controllers.Base;
using BlogManagementDomain.Blog;
using BlogManagementDomain.Dto.Request;
using BlogManagementDomain.Dto.Response;
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
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CommentsController(
                ICommentService commentService,
                ILogger<CommentsController> logger, IMapper mapper)
            : base(logger)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        #endregion

        #region Endpoints
        [HttpPost]
        public async Task<IActionResult> AddComment(int postId, [FromBody] CommentRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _logger.LogInformation("Adding a new comment to post with ID: {PostId}", postId);
                        
                var comment = _mapper.Map<CommentDomain>(dto);
                comment.BlogPostId = postId;

                await _commentService.AddCommentAsync(comment);
                     
                var response = _mapper.Map<CommentResponseDTO>(comment);

                return CreatedAtAction(nameof(AddComment), new { postId, id = response.Id }, response);
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

                var response = _mapper.Map<IEnumerable<CommentResponseDTO>>(comments);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"Failed to fetch comments for post with ID: {postId}");
            }
        }

        #endregion
    }
}