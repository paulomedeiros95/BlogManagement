using BlogManagementDomain.Blog;
using BlogManagementInfra.Repository.Base;
using BlogManagementInfra.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementInfra.Repository
{
    public class CommentRepository : BaseRepository<CommentDomain>, ICommentRepository
    {
        public CommentRepository(DbContext context) : base(context)
        {
        }
    }
}
