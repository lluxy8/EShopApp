using Core.Common.BaseClasses;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FakeWriteRepository<T> : WriteRepository<T> where T : BaseEntity
    {
        private readonly List<T> _data = new();

        public FakeWriteRepository(WriteDbContext context) : base(context) { }

        public override async Task<T?> GetByConditionAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellation)
        {
            return _data.FirstOrDefault(predicate.Compile());
        }

        public void AddTestData(params T[] items) => _data.AddRange(items);
    }
}
