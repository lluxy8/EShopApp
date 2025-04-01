using Core.Common.BaseClasses;
using Core.Entities.Write;
using Core.Helpers;
using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class CachedReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly IReadRepository<T> _decorated;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public CachedReadRepository(IReadRepository<T> decorated, IMemoryCache cache)
        {
            _decorated = decorated;
            _cache = cache;
            _cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(GetCacheDuration(typeof(T)));
        }

        private static TimeSpan GetCacheDuration(Type entityType)
        {
            return entityType.Name switch
            {
                nameof(Product) => TimeSpan.FromMinutes(15),
                nameof(Category) => TimeSpan.FromDays(1),
                _ => TimeSpan.FromMinutes(30)
            };
        }


        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            string cacheKey = $"cache-{typeof(T).Name}-{id}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetOptions(_cacheOptions);
                var result = await _decorated.GetByIdAsync(id, cancellation);
                if (result == null) entry.AbsoluteExpiration = DateTimeOffset.Now; // Don't cache nulls
                return result;
            });
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellation)
        {
            string cacheKey = $"cache-{typeof(T).Name}-all";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetOptions(_cacheOptions);
                return await _decorated.GetAllAsync(cancellation);
            }) ?? new List<T>();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation)
        {
            string predicateHash = predicate.ToHash<T>(); 
            string cacheKey = $"cache-{typeof(T).Name}-any-{predicateHash}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetOptions(_cacheOptions);
                return await _decorated.AnyAsync(predicate, cancellation);
            });
        }

        public async Task<T?> GetByConditionAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellation)
        {
            string predicateHash = predicate.ToHash<T>();

            string cacheKey = $"cache-{typeof(T).Name}-condition-{predicateHash}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetOptions(_cacheOptions); 
                return await _decorated.GetByConditionAsync(predicate, cancellation); 
            });
        }

        public IQueryable<T> GetQueryable(bool tracking = false) =>
            _decorated.GetQueryable(tracking);
    }
}
