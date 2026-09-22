using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrendSense.Application.Interfaces;
using TrendSense.Infrastructure.Caching;
using TrendSense.Infrastructure.Moex;

namespace TrendSense.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["ConnectionStrings:Redis"];
            });

            services.AddHttpClient<IStockMarketService, MoexStockMarketService>(client =>
            {
                client.BaseAddress = new Uri("https://iss.moex.com/iss/");
            });

            services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }
    }
}
