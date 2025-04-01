using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReadDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("ReadDb"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddDbContext<WriteDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("WriteDb")));

            return services;
        }
    }
}
