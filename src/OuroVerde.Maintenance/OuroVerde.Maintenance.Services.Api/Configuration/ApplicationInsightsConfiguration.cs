namespace Unidas.MS.Maintenance.Services.Api.Configuration
{
    public static class ApplicationInsightsConfiguration
    {
        public static void AddApplicationInsightsConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            
        }
    }
}
