using BlogManagementService.Comment;
using BlogManagementService.Interfaces;
using BlogManagementService.Post;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagementService.DependencyInjection
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {            
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}
