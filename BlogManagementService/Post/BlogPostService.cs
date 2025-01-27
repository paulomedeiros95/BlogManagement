using BlogManagementDomain.Domain;
using BlogManagementService.Interfaces;

namespace BlogManagementService.Post
{
    public class BlogPostService : IBlogPostService
    {
        public Task AddPostAsync(BlogPostDomain blogPost)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPostDomain>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPostDomain> GetPostByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
