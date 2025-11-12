using ETR.Nine.Services.Forex.Application.Interfaces.IServices;
using ETR.Nine.Services.Forex.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETR.Nine.Services.Forex.Application
{
    public static class Dependencies
    {
        public static IServiceCollection AddApplication(
        this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IForexService, ForexService>();
            return services;
        }
    }
}