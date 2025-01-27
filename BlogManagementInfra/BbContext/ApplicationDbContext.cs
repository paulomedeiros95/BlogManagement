using BlogManagementDomain.Blog;
using BlogManagementDomain.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
