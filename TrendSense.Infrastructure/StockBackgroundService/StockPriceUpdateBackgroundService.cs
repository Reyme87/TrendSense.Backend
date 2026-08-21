using Microsoft.Extensions.Hosting;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TrendSense.Application.Features.Stocks.Commands.UpdateStockPrices;

namespace TrendSense.Infrastructure.StockBackgroundService
{
    public class StockPriceUpdateBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<StockPriceUpdateBackgroundService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public StockPriceUpdateBackgroundService(IServiceScopeFactory scopeFactory, ILogger<StockPriceUpdateBackgroundService> logger) =>
            (_scopeFactory, _logger) = (scopeFactory, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Stock price update background service started.");

            using var timer = new PeriodicTimer(_interval);

            while(await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    await UpdatePricesAsync(stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating stock prices.");
                }
            }

            _logger.LogInformation("Stock price update background service stopped.");
        }

        private async Task UpdatePricesAsync(CancellationToken stoppingToken)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(new UpdateStockPricesCommand(), stoppingToken);
        }
    }
}
