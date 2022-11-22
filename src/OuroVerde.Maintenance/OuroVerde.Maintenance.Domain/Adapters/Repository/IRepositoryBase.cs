using OuroVerde.Maintenance.Domain.Core.Domain;

namespace OuroVerde.Maintenance.Domain.Adapters.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(Guid id);
    }
}

