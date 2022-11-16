using Unidas.MS.Maintenance.Infra.CrossCutting.IoC;

namespace Unidas.MS.Maintenance.Services.Api.Configuration
{
    public static class DependecyConfiguration
    {
        public static void AddDependecyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}

