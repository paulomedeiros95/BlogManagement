using BlogManagementDomain.Domain;
using BlogManagementInfra.BbContext;
using BlogManagementInfra.Repository.Base;
using BlogManagementInfra.Repository.Interface;

namespace BlogManagementInfra.Repository
{
    public class BlogPostRepository : BaseRepository<BlogPostDomain>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
