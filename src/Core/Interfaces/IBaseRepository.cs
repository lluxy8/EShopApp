using Core.Common.BaseClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation);
        IQueryable<T> GetQueryable(bool tracking = false);
    }
}
