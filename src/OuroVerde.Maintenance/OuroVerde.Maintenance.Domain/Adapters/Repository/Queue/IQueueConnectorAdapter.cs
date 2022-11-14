using OuroVerde.Maintenance.Domain.Core.Domain;

namespace Unidas.MS.Maintenance.Domain.Adapters.Repository.Queue;

public interface IQueueConnectorAdapter
{
    Task<List<T>> ConsumeMessageObject<T>(string sbConnectionString, string sbQueueName);

    Task<List<T>> ConsumeMessageObject<T>();
}
