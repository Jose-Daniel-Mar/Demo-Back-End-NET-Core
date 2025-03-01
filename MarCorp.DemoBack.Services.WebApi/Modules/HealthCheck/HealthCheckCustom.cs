using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MarCorp.DemoBack.Services.WebApi.Modules.HealthCheck
{
    /// <summary>
    /// Custom health check implementation for MarCorp DemoBack Services.
    /// </summary>
    public class HealthCheckCustom : IHealthCheck
    {
        private readonly Random _random = new();

        /// <summary>
        /// Performs a health check and returns the result.
        /// </summary>
        /// <param name="context">The context in which the health check is being executed.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the health check.</param>
        /// <returns>A task that represents the health check result.</returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // Simulate a response time between 1 and 300 milliseconds.
            // TODO: Replace this with a real health check.
            var responseTime = _random.Next(1, 300);
            
            if (responseTime < 100)
            {
                return Task.FromResult(HealthCheckResult.Healthy("MarCorp Healthy result from HealthCheckCustom"));
            }
            else if (responseTime < 200)
            {
                return Task.FromResult(HealthCheckResult.Degraded("MarCorp Degraded result from HealthCheckCustom"));
            }
            return Task.FromResult(HealthCheckResult.Unhealthy("MarCorp Unhealthy result from HealthCheckCustom"));
        }
    }
}
