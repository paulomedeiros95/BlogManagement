using BlogManagementDomain.Domain;
using BlogManagementInfra.Repository.Base;
using BlogManagementInfra.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementInfra.Repository
{
    public class BlogPostRepository : BaseRepository<BlogPostDomain>, IBlogPostRepository
    {
        public BlogPostRepository(DbContext context) : base(context)
        {
        }
    }
}
