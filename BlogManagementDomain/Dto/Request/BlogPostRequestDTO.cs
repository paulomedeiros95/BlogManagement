using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementDomain.Dto.Request
{
    public class BlogPostRequestDTO
    {

        /// <summary>
        /// Title of the blog post.
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Content of the blog post.
        /// </summary>
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }
    }
}
