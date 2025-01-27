using BlogManagementDomain.Domain;

namespace BlogManagementService.Interfaces
{
    public interface IBlogPostService
    {
        /// <summary>
        /// Return all posts.
        /// </summary>
        Task<IEnumerable<BlogPostDomain>> GetAllPostsAsync();

        /// <summary>
        /// Return post by Id.
        /// </summary>
        /// <param name="id">Post Id/param>
        Task<BlogPostDomain> GetPostByIdAsync(int id);

        /// <summary>
        /// Add new post.
        /// </summary>
        /// <param name="blogPost">Entidade BlogPost</param>
        Task AddPostAsync(BlogPostDomain blogPost);
    }
}
