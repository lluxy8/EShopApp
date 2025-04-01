using Core.Common.BaseClasses;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class WriteRepository<T> : BaseRepository<T>, IWriteRepository<T> where T : BaseEntity
    {
        public WriteRepository(WriteDbContext context) : base(context) { }

        public async Task AddAsync(T entity, CancellationToken cancellation = default) =>
            await _context.Set<T>().AddAsync(entity, cancellation);

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellation = default) =>
            _context.Set<T>().AddRangeAsync(entities, cancellation);

        public Task DeleteAsync(T entity, CancellationToken cancellation = default)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellation = default)
        {
            _context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity, CancellationToken cancellation = default)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellation = default)
        {
            _context.Set<T>().UpdateRange(entities);
            return Task.CompletedTask;
        }
    }
}
