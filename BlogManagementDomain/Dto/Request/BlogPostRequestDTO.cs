using System.ComponentModel.DataAnnotations;

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
