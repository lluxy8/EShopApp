using Core.Common.BaseClasses;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByConditionAsync(Expression<Func<T, bool>> predicate, 
            CancellationToken cancellation) =>
            await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellation);

        public IQueryable<T> GetQueryable(bool tracking = false)
        {
            var query = _context.Set<T>().AsQueryable();
            return tracking ? query : query.AsNoTracking();
        }
    }
}
