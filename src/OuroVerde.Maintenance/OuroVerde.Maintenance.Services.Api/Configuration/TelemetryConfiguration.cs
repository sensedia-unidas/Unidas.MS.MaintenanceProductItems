namespace Unidas.MS.Maintenance.Services.Api.Configuration
{
    public static class TelemetryConfiguration
    {
        public static void AddApplicationInsightsTelemetry(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
        }
    }
}
