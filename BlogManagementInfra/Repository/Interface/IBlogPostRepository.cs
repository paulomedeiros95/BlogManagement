using BlogManagementDomain.Domain;
using BlogManagementInfra.BbContext;
using BlogManagementInfra.Repository.Interface.Base;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementInfra.Repository.Interface
{
    public interface IBlogPostRepository : IBaseRepository<BlogPostDomain>
    {        
    }
}
