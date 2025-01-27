using System.ComponentModel.DataAnnotations;

namespace BlogManagementDomain.Dto.Request
{
    public class CommentRequestDTO
    {
        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        public string Content { get; set; }
    }
}
