using BlogManagementDomain.Domain;
using BlogManagementInfra.Repository.Interface;
using BlogManagementService.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogManagementService.Post
{
    public class BlogPostService : IBlogPostService
    {
        #region Fields
        private readonly IBlogPostRepository _repository;
        private readonly ILogger<BlogPostService> _logger;
        #endregion

        #region Constructor

        public BlogPostService(IBlogPostRepository repository, ILogger<BlogPostService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        #endregion

        public async Task<IEnumerable<BlogPostDomain>> GetAllPostsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all blog posts from the repository.");
                return await _repository.GetAllAsync(x => x.DeletedAt != null, "Comments");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all blog posts.");
                throw;
            }
        }

        public async Task<BlogPostDomain> GetPostByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching blog post with ID: {Id}", id);
                var result = await _repository.FindByIdAsync(x => x.Id == id, "Comments");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching blog post with ID: {Id}", id);
                throw;
            }
        }

        public async Task AddPostAsync(BlogPostDomain blogPost)
        {
            try
            {
                _logger.LogInformation("Adding a new blog post: {Title}", blogPost.Title);
                await _repository.AddAsync(blogPost);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding a new blog post: {Title}", blogPost.Title);
                throw;
            }
        }
    }
}

