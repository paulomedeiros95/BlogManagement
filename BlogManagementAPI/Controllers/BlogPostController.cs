using AutoMapper;
using BlogManagementAPI.Controllers.Base;
using BlogManagementDomain.Domain;
using BlogManagementDomain.Dto.Request;
using BlogManagementDomain.Dto.Response;
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
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BlogPostsController(
                IBlogPostService blogPostService, 
                ILogger<BlogPostsController> logger, IMapper mapper)
                : base(logger)
        {
            _blogPostService = blogPostService;
            _mapper = mapper;
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
                                
                var response = posts.Select(p => new
                {
                    p.Id,
                    p.Title,
                    CommentsCount = p.Comments.Count
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex, "Failed to fetch blog posts.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogPostRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _logger.LogInformation("Creating a new blog post: {Title}", dto.Title);

                var post = _mapper.Map<BlogPostDomain>(dto);

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

                var response = new
                {
                    post.Id,
                    post.Title,
                    post.Content,
                    Comments = post.Comments.Select(c => new { c.Id, c.Content })
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex, $"Failed to fetch blog post with ID: {id}");
            }
        }

        #endregion
    }
}
