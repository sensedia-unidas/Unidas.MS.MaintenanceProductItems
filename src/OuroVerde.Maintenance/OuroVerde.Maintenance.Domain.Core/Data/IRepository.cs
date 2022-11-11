using OuroVerde.Maintenance.Domain.Core.Domain;

namespace OuroVerde.Maintenance.Domain.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}