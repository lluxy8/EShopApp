using Core.Common.BaseClasses;
using Core.Common.Result;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReadRepository<T> : BaseRepository<T>, IReadRepository<T> where T : BaseEntity
    {
        public ReadRepository(ReadDbContext context) : base(context) { }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation = default) =>
            _context.Set<T>().AsNoTracking().AnyAsync(predicate, cancellation);

        public async Task<List<T>> GetAllAsync(CancellationToken cancellation = default) =>
            await _context.Set<T>().AsNoTracking().ToListAsync(cancellation);

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellation = default) =>
            await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellation);
    }
}
