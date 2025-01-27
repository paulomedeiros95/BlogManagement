using BlogManagementDomain.Domain;
using BlogManagementInfra.Repository.Interface;
using BlogManagementService.Interfaces;
using BlogManagementService.Post;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementTests.BlogPost
{
    [TestFixture]
    public class BlogPostServiceTests
    {
        private IBlogPostRepository _repository;
        private ILogger<BlogPostService> _logger;
        private IBlogPostService _blogPostService;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IBlogPostRepository>();
            _logger = Substitute.For<ILogger<BlogPostService>>();
            _blogPostService = new BlogPostService(_repository, _logger);
        }

        [Test]
        public async Task GetAllPostsAsync_ShouldReturnAllPosts()
        {
            // Arrange
            var posts = new List<BlogPostDomain>
            {
                new BlogPostDomain { Id = 1, Title = "Post 1", Content = "Content 1" },
                new BlogPostDomain { Id = 2, Title = "Post 2", Content = "Content 2" }
            };

            _repository.GetAllAsync().Returns(posts);

            // Act
            var result = await _blogPostService.GetAllPostsAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Post 1", result.First().Title);
            _logger.Received(1).LogInformation("Fetching all blog posts from the repository.");
        }

        [Test]
        public async Task GetPostByIdAsync_ShouldReturnCorrectPost()
        {
            // Arrange
            var post = new BlogPostDomain { Id = 1, Title = "Post 1", Content = "Content 1" };

            _repository.FindByIdAsync(1).Returns(post);

            // Act
            var result = await _blogPostService.GetPostByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Post 1", result.Title);
            _logger.Received(1).LogInformation("Fetching blog post with ID: {Id}", 1);
        }

        [Test]
        public async Task GetPostByIdAsync_WhenPostDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            _repository.FindByIdAsync(99).Returns((BlogPostDomain)null);

            // Act
            var result = await _blogPostService.GetPostByIdAsync(99);

            // Assert
            Assert.IsNull(result);
            _logger.Received(1).LogInformation("Fetching blog post with ID: {Id}", 99);
        }

        [Test]
        public void GetPostByIdAsync_WhenRepositoryThrowsException_LogsErrorAndRethrows()
        {
            // Arrange
            var exceptionMessage = "Repository failure";
            _repository.FindByIdAsync(1).Throws(new System.Exception(exceptionMessage));

            // Act & Assert
            var exception = Assert.ThrowsAsync<System.Exception>(() => _blogPostService.GetPostByIdAsync(1));
            Assert.AreEqual(exceptionMessage, exception.Message);
            _logger.Received(1).LogError(Arg.Any<System.Exception>(), "Error while fetching blog post with ID: {Id}", 1);
        }

        [Test]
        public async Task AddPostAsync_ShouldCallRepositoryAdd()
        {
            // Arrange
            var newPost = new BlogPostDomain { Title = "New Post", Content = "New Content" };

            // Act
            await _blogPostService.AddPostAsync(newPost);

            // Assert
            await _repository.Received(1).AddAsync(newPost);
            _logger.Received(1).LogInformation("Adding a new blog post: {Title}", newPost.Title);
        }

        [Test]
        public void AddPostAsync_WhenRepositoryThrowsException_LogsErrorAndRethrows()
        {
            // Arrange
            var newPost = new BlogPostDomain { Title = "New Post", Content = "New Content" };
            _repository.When(x => x.AddAsync(newPost))
                       .Throw(new System.Exception("Repository failure"));

            // Act & Assert
            var exception = Assert.ThrowsAsync<System.Exception>(() => _blogPostService.AddPostAsync(newPost));
            Assert.AreEqual("Repository failure", exception.Message);
            _logger.Received(1).LogError(Arg.Any<System.Exception>(), "Error while adding a new blog post: {Title}", newPost.Title);
        }
    }
}