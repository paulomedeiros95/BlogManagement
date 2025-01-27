using BlogManagementInfra.BbContext;
using BlogManagementInfra.Repository.Interface.Base;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementInfra.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Fields
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        #endregion

        #region Constructor

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        #endregion

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
