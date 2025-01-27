using BlogManagementDomain.Base;
using BlogManagementDomain.Blog;
using System.ComponentModel.DataAnnotations;

namespace BlogManagementDomain.Domain
{
    public class BlogPostDomain : BaseDomain
    {
        #region Properties

        [Required]
        [MaxLength(70)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        public ICollection<CommentDomain> Comments { get; set; } = new List<CommentDomain>();

        #endregion
    }
}
