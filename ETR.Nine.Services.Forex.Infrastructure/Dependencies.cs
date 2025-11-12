using ETR.Nine.Services.Forex.Infrastructure.Persistence;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;
using ETR.Nine.Services.Forex.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETR.Nine.Services.Forex.Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SQLiteConnection");
            services.AddDbContext<ForexDbContext>(options =>
                options.UseSqlite(connectionString));
            services.AddScoped<IAppDbContext, ForexDbContext>();
            services.AddScoped<IForexRepository, ForexRepository>();
            services.AddScoped<IForexService, ForexService>();

            return services;
        }
    }
}