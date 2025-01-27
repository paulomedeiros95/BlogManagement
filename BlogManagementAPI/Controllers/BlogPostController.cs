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

                var response = _mapper.Map<IEnumerable<BlogPostResponseDTO>>(posts);
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

                var response = _mapper.Map<BlogPostResponseDTO>(post);

                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
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

                // Mapear a entidade de domínio para DTO de resposta
                var response = _mapper.Map<BlogPostResponseDTO>(post);

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
