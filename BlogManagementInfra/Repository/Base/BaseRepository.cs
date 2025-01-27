using BlogManagementInfra.BbContext;
using BlogManagementInfra.Repository.Interface.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> whereByExpression, params string[] relations)
        {
            var query = _context.Set<T>().AsQueryable();

            AddIncludes(relations, ref query);

            return await query.ToListAsync();
        }

        private static void AddIncludes(string[] relations, ref IQueryable<T> query)
        {
            if (relations == null || !relations.Any())
                return;

            foreach (var relation in relations)
            {
                query = query.Include(relation);
            }
        }

        public async Task<T> FindByIdAsync(Expression<Func<T, bool>> whereByExpression, params string[] relations)
        {
            var query = _context.Set<T>().AsQueryable();

            AddIncludes(relations, ref query);

            return await query.FirstOrDefaultAsync(whereByExpression);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
