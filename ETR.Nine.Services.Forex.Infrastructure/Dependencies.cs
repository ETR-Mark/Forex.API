using ETR.Nine.Services.Forex.Infrastructure.Persistence;
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

            return services;
        }
    }
}