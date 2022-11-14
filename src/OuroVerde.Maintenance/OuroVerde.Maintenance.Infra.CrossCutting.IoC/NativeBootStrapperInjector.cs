using Microsoft.Extensions.DependencyInjection;
using OuroVerde.Maintenance.Application.Interface;
using OuroVerde.Maintenance.Domain.Core.Mediator;
using Unidas.MS.Maintenance.Application.AppServices;
using Unidas.MS.Maintenance.Domain.Adapters.Repository.Queue;
using Unidas.MS.Maintenance.Infra.Data.Queue;

namespace Unidas.MS.Maintenance.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //DbContext  

            //Application
            services.AddScoped<IItensIntegrationService, ItensIntegrationService>();

            //Repository
            services.AddScoped<IQueueConnectorAdapter, QueueConnectorAdapter>();
        }
    }
}