using System.Linq.Expressions;

namespace BlogManagementInfra.Repository.Interface.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> whereByExpression, params string[] relations);
        Task<T> FindByIdAsync(Expression<Func<T, bool>> filter = null, params string[] relations);
        Task AddAsync(T entity);
    }
}
