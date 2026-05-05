using Microsoft.Extensions.Diagnostics.HealthChecks;
using RecordShop.Api.Data;

namespace RecordShop.Api.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly RecordShopContext _context;

        public DatabaseHealthCheck(RecordShopContext context)
        {
            _context = context; 
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);

                return HealthCheckResult.Healthy("Database is responding");
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy($"Database check failed, unhealthy :(");
            }
        }
    }
}
