using Microsoft.Extensions.DependencyInjection;
using TrendSense.Application.Interfaces;
using TrendSense.Infrastructure.Moex;

namespace TrendSense.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient<IStockMarketService, MoexStockMarketService>(client =>
            {
                client.BaseAddress = new Uri("https://iss.moex.com/iss/");
            });

            return services;
        }
    }
}
