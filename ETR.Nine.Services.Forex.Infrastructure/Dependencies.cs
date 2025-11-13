using ETR.Nine.Services.Forex.Infrastructure.Persistence;
using ETR.Nine.Services.Forex.Infrastructure.Repositories;
using ETR.Nine.Services.Forex.Infrastructure.Repositories.Forex;
using ETR.Nine.Services.Forex.Infrastructure.Services;
using ETR.Nine.Services.Forex.Infrastructure.Services.Forex;
using ETR.Nine.Services.Forex.Infrastructure.Settings;
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
            services.AddScoped<IExternalForexRepository, ExternalForexRepository>();
            services.AddScoped<IForexService, ForexService>();
            services.AddScoped<IExternalForexService, ExternalForexService>();

            services.Configure<ExternalForexAPISettings>(
                configuration.GetSection("ForexApi")
            );

            return services;
        }
    }
}