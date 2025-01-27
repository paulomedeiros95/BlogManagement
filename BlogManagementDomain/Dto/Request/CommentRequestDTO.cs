using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementDomain.Dto.Request
{
    public class CommentRequestDTO
    {
        /// <summary>
        /// Content of the comment.
        /// </summary>
        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        public string Content { get; set; }

        /// <summary>
        /// ID of the blog post this comment is associated with.
        /// </summary>
        [Required(ErrorMessage = "BlogPostId is required.")]
        public int BlogPostId { get; set; }
    }
}
