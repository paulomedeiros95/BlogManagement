namespace BlogManagementDomain.Dto.Response
{
    public class CommentResponseDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BlogPostId { get; set; }
    }
}
