using BlogManagementDomain.Domain;
using BlogManagementInfra.BbContext;
using BlogManagementInfra.Repository.Base;
using BlogManagementInfra.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementInfra.Repository
{
    public class BlogPostRepository : BaseRepository<BlogPostDomain>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BlogPostDomain>> GetPostsWithCommentsAsync()
        {
            return await _dbSet.Include(p => p.Comments).ToListAsync();
        }
    }
}
