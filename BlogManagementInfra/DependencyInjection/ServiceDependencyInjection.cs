using BlogManagementInfra.Repository;
using BlogManagementInfra.Repository.Base;
using BlogManagementInfra.Repository.Interface;
using BlogManagementInfra.Repository.Interface.Base;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagementInfra.DependencyInjection
{
    public static class RepositoryDependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            return services;
        }
    }
}
