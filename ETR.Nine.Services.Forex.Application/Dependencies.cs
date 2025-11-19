using ETR.Nine.Services.Forex.Application.Forex.Queries.GetAllForex;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETR.Nine.Services.Forex.Application
{
    public static class Dependencies
    {
        public static IServiceCollection AddApplication(
        this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGetAllForexHandler, GetAllForexHandler>();
            return services;
        }
    }
}