using BlogManagementDomain.Base;
using BlogManagementDomain.Domain;
using System.ComponentModel.DataAnnotations;

namespace BlogManagementDomain.Blog
{
    public class CommentDomain : BaseDomain
    {
        #region Properties

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
        public int BlogPostId { get; set; }
        public BlogPostDomain BlogPost { get; set; }
        #endregion
    }
}
