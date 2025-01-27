using BlogManagementDomain.Blog;
using BlogManagementInfra.Repository.Interface;
using BlogManagementService.Comment;
using BlogManagementService.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace BlogManagementTests.Comments
{
    [TestFixture]
    public class CommentServiceTests
    {
        private ICommentRepository _repository;
        private ILogger<CommentService> _logger;
        private ICommentService _commentService;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ICommentRepository>();
            _logger = Substitute.For<ILogger<CommentService>>();
            _commentService = new CommentService(_repository, _logger);
        }

        [Test]
        public async Task AddCommentAsync_ValidComment_CallsRepositoryAdd()
        {
            // Arrange
            var comment = new CommentDomain
            {
                Id = 1,
                Content = "This is a test comment",
                BlogPostId = 10
            };

            // Act
            await _commentService.AddCommentAsync(comment);

            // Assert
            await _repository.Received(1).AddAsync(comment);
            _logger.Received(1).LogInformation("Adding a new comment for post with ID: {PostId}", comment.BlogPostId);
            _logger.Received(1).LogInformation("Comment added successfully with ID: {CommentId}", comment.Id);
        }

        [Test]
        public void AddCommentAsync_WhenRepositoryThrowsException_LogsErrorAndRethrows()
        {
            // Arrange
            var comment = new CommentDomain
            {
                Id = 1,
                Content = "This is a test comment",
                BlogPostId = 10
            };

            _repository.When(x => x.AddAsync(comment))
                       .Throw(new System.Exception("Repository failure"));

            // Act & Assert
            var exception = Assert.ThrowsAsync<System.Exception>(() => _commentService.AddCommentAsync(comment));

            Assert.AreEqual("Repository failure", exception.Message);
            _logger.Received(1).LogError(Arg.Any<System.Exception>(), "Error while adding a new comment for post with ID: {PostId}", comment.BlogPostId);
        }

        [Test]
        public async Task GetCommentsByPostIdAsync_ExistingPostId_ReturnsComments()
        {
            // Arrange
            var postId = 10;
            var comments = new List<CommentDomain>
            {
                new CommentDomain { Id = 1, Content = "Comment 1", BlogPostId = postId },
                new CommentDomain { Id = 2, Content = "Comment 2", BlogPostId = postId }
            };

            _repository.GetAllAsync(x => x.BlogPostId == postId, "Comments").Returns(comments);

            // Act
            var result = await _commentService.GetCommentsByPostIdAsync(postId);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Comment 1", result.First().Content);
            _logger.Received(1).LogInformation("Fetching comments for post with ID: {PostId}", postId);
            _logger.Received(1).LogInformation("Successfully fetched {Count} comments for post with ID: {PostId}");
        }

        [Test]
        public async Task GetCommentsByPostIdAsync_NonExistingPostId_ReturnsEmptyList()
        {
            // Arrange
            var postId = 99;
            _repository.GetAllAsync(x => x.BlogPostId == postId, "Comments").Returns(new List<CommentDomain>());

            // Act
            var result = await _commentService.GetCommentsByPostIdAsync(postId);

            // Assert
            Assert.IsEmpty(result);
            _logger.Received(1).LogInformation("Fetching comments for post with ID: {PostId}", postId);
            _logger.Received(1).LogInformation("Successfully fetched {Count} comments for post with ID: {PostId}");
        }

        [Test]
        public void GetCommentsByPostIdAsync_WhenRepositoryThrowsException_LogsErrorAndRethrows()
        {
            // Arrange
            var postId = 10;
            _repository.When(x => x.GetAllAsync(x => x.BlogPostId == postId, "Comments"))
                       .Throw(new System.Exception("Repository failure"));

            // Act & Assert
            var exception = Assert.ThrowsAsync<System.Exception>(() => _commentService.GetCommentsByPostIdAsync(postId));

            Assert.AreEqual("Repository failure", exception.Message);
            _logger.Received(1).LogError(Arg.Any<System.Exception>(), "Error while fetching comments for post with ID: {PostId}", postId);
        }
    }
}