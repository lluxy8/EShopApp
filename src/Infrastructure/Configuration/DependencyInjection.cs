using Core.Common.BaseClasses;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var entityTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseEntity)));

            foreach (var entityType in entityTypes)
            {
                var readRepoType = typeof(ReadRepository<>).MakeGenericType(entityType);
                var readRepoInterface = typeof(IReadRepository<>).MakeGenericType(entityType);

                services.AddScoped(readRepoInterface, readRepoType);

                var cachedRepoType = typeof(CachedReadRepository<>).MakeGenericType(entityType);
                services.Decorate(readRepoInterface, cachedRepoType);
            }

            services.AddScoped(typeof(IBaseRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            services.AddScoped<IReadDbUnitOfWork, ReadDbUnitOfWork>();
            services.AddScoped<IWriteDbUnitOfWork, WriteDbUnitOFWork>();

            services.AddDbContext<ReadDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("ReadDb"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddDbContext<WriteDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("WriteDb")));

            return services;
        }
    }
}
