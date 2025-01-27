using BlogManagementDomain.Blog;
using BlogManagementDomain.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementInfra.BbContext
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        #endregion

        #region DbSet
        public DbSet<BlogPostDomain> Blogs { get; set; }
        public DbSet<CommentDomain> Comments { get; set; }

        #endregion
    }
}
