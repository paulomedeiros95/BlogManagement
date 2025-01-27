using BlogManagementDomain.Domain;
using BlogManagementService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementAPI.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        #region Fields
        private readonly IBlogPostService _blogPostService;
        #endregion

        #region Constructor
        public BlogPostsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }
        #endregion

        #region Endpoints
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _blogPostService.GetAllPostsAsync();
            return Ok(posts.Select(p => new { p.Id, p.Title, CommentsCount = p.Comments.Count }));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogPostDomain post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _blogPostService.AddPostAsync(post);
            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _blogPostService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();

            return Ok(new
            {
                post.Id,
                post.Title,
                post.Content,
                Comments = post.Comments.Select(c => new { c.Id, c.Content })
            });
        }

        #endregion
    }
}
