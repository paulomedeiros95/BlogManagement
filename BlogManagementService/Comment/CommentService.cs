using BlogManagementDomain.Blog;
using BlogManagementInfra.Repository.Interface;
using BlogManagementService.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogManagementService.Comment
{public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly ILogger<CommentService> _logger;

        public CommentService(ICommentRepository repository, ILogger<CommentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task AddCommentAsync(CommentDomain comment)
        {
            try
            {
                _logger.LogInformation("Adding a new comment for post with ID: {PostId}", comment.BlogPostId);
                await _repository.AddAsync(comment);
                _logger.LogInformation("Comment added successfully with ID: {CommentId}", comment.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding a new comment for post with ID: {PostId}", comment.BlogPostId);
                throw;
            }
        }

        public async Task<IEnumerable<CommentDomain>> GetCommentsByPostIdAsync(int postId)
        {
            try
            {
                _logger.LogInformation("Fetching comments for post with ID: {PostId}", postId);
                var comments = await _repository.FindAllAsync(x => x.BlogPostId == postId);
                _logger.LogInformation("Successfully fetched {Count} comments for post with ID: {PostId}");
                return comments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching comments for post with ID: {PostId}", postId);
                throw;
            }
        }
    }
}