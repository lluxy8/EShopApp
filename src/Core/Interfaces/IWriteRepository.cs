using Core.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IWriteRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity, CancellationToken cancellation = default);
        Task UpdateAsync(T entity, CancellationToken cancellation = default);
        Task DeleteAsync(T entity, CancellationToken cancellation = default);
        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellation = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellation = default);
        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellation = default);

    }
}
