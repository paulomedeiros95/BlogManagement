using BlogManagementAPI.Controllers.Base;
using BlogManagementDomain.Domain;
using BlogManagementService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementAPI.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class BlogPostsController : BaseController
    {
        #region Fields
        private readonly IBlogPostService _blogPostService;
        #endregion

        #region Constructor
        public BlogPostsController(IBlogPostService blogPostService, ILogger<BlogPostsController> logger)
                : base(logger)
        {
            _blogPostService = blogPostService;
        }
        #endregion

        #region Endpoints
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all blog posts.");
                var posts = await _blogPostService.GetAllPostsAsync();
                return Ok(posts.Select(p => new { p.Id, p.Title, CommentsCount = p.Comments.Count }));
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to fetch blog posts.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogPostDomain post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _logger.LogInformation("Creating a new blog post: {Title}", post.Title);
                await _blogPostService.AddPostAsync(post);
                return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to create a new blog post.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching blog post with ID: {Id}", id);
                var post = await _blogPostService.GetPostByIdAsync(id);
                if (post == null)
                {
                    _logger.LogWarning("Blog post with ID {Id} not found.", id);
                    return NotFound();
                }

                return Ok(new
                {
                    post.Id,
                    post.Title,
                    post.Content,
                    Comments = post.Comments.Select(c => new { c.Id, c.Content })
                });
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"Failed to fetch blog post with ID: {id}");
            }
        }

        #endregion
    }
}
