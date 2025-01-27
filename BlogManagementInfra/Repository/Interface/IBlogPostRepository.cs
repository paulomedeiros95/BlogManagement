using BlogManagementDomain.Domain;
using BlogManagementInfra.Repository.Interface.Base;

namespace BlogManagementInfra.Repository.Interface
{
    public interface IBlogPostRepository : IBaseRepository<BlogPostDomain>
    {
    }
}
