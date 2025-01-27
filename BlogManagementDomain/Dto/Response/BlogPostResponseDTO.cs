using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementDomain.Dto.Response
{
    public class BlogPostResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CommentsCount { get; set; }
        public IEnumerable<CommentResponseDTO> Comments { get; set; }
    }
}
