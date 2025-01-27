using BlogManagementDomain.Base;
using BlogManagementDomain.Domain;

namespace BlogManagementDomain.Blog
{
    public class CommentDomain : BaseDomain
    {
        public string Content { get; set; }
        public int BlogPostId { get; set; }
        public BlogDomain BlogPost { get; set; }
    }
}
