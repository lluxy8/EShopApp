using Core.Common.BaseClasses;
using Core.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReadRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
        Task<List<T>> GetAllAsync( CancellationToken cancellation = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation = default);
    }
}
