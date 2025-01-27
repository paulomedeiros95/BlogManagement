using BlogManagementDomain.Blog;

namespace BlogManagementService.Interfaces
{
    public interface ICommentService
    {
        /// <summary>
        /// Add new comment into a post.
        /// </summary>
        /// <param name="comment">Comment Domain</param>
        Task AddCommentAsync(CommentDomain comment);

        /// <summary>
        /// Return all comments made in a especific post.
        /// </summary>
        /// <param name="postId">post ID</param>
        Task<IEnumerable<CommentDomain>> GetCommentsByPostIdAsync(int postId);
    }
}
