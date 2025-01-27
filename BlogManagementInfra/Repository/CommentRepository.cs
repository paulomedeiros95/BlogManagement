using BlogManagementDomain.Blog;
using BlogManagementInfra.BbContext;
using BlogManagementInfra.Repository.Base;
using BlogManagementInfra.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementInfra.Repository
{
    public class CommentRepository : BaseRepository<CommentDomain>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
