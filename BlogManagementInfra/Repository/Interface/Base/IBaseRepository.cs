using System.Linq.Expressions;

namespace BlogManagementInfra.Repository.Interface.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task AddAsync(T entity);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> filter = null);
    }
}
