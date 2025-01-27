using BlogManagementDomain.Blog;
using BlogManagementService.Interfaces;

namespace BlogManagementService.Comment
{
    public class CommentService : ICommentService
    {
        public Task AddCommentAsync(CommentDomain comment)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommentDomain>> GetCommentsByPostIdAsync(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
