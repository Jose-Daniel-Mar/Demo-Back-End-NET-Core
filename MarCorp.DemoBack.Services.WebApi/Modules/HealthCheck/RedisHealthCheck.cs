using MarCorp.DemoBack.Support.Common;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace MarCorp.DemoBack.Services.WebApi.Modules.HealthCheck
{
    /// <summary>
    /// Health check for Redis.
    /// </summary>
    public class RedisHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;
        private readonly IAppLogger<RedisHealthCheck> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisHealthCheck"/> class.
        /// </summary>
        /// <param name="configuration">The configuration to use for connecting to Redis.</param>
        /// <param name="logger"></param>
        public RedisHealthCheck(IConfiguration configuration, IAppLogger<RedisHealthCheck> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Checks the health of the Redis connection.
        /// </summary>
        /// <param name="context">The context for the health check.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the health check.</param>
        /// <returns>A <see cref="HealthCheckResult"/> representing the result of the health check.</returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Forzar una nueva conexión cada vez
                using (var connection = await ConnectionMultiplexer.ConnectAsync(_configuration.GetConnectionString("RedisConnection")))
                {
                    if (!connection.IsConnected)
                    {
                        _logger.LogWarning("La conexión a Redis no está activa.");
                        return HealthCheckResult.Unhealthy("La conexión a Redis no está activa.");
                    }

                    // Verificar que podemos ejecutar un comando simple
                    var db = connection.GetDatabase();
                    var pong = await db.PingAsync();

                    _logger.LogInformation($"Redis respondió en {pong.TotalMilliseconds}ms");
                    return HealthCheckResult.Healthy($"Redis respondió en {pong.TotalMilliseconds}ms");
                }
            }
            catch (RedisConnectionException ex)
            {
                _logger.LogWarning($"No se pudo conectar a Redis: {ex.Message}");
                return HealthCheckResult.Unhealthy($"No se pudo conectar a Redis: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error al verificar Redis: {ex.Message}");
                return HealthCheckResult.Unhealthy($"Error al verificar Redis: {ex.Message}");
            }
        }
    }
}