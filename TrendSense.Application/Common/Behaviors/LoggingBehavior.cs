using MediatR;
using Serilog;
using System.Diagnostics;
using TrendSense.Application.Interfaces;

namespace TrendSense.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest
    {
        private readonly ICurrentUserService _currentUser;

        public LoggingBehavior(ICurrentUserService currentUser) => _currentUser = currentUser;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUser.UserId;
            var stopwatch = Stopwatch.StartNew();

            Log.Information("Handling Request: {@RequestName} {@UserId} {@Request}", requestName, userId, request);

            try
            {
                var response = await next();

                stopwatch.Stop();

                Log.Information("Handled {@RequestName} in {ElapsedMilliseconds} ms", requestName, stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                Log.Information("Request {@RequestName} failed after {@EllapsedMilliseconds} ms with Error {@ErrorMessage}", requestName, stopwatch.ElapsedMilliseconds, ex.Message);

                throw;
            }
        }
    }
}
