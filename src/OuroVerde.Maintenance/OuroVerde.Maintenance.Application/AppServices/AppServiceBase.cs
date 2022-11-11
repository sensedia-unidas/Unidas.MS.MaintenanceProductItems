using AutoMapper;
using OuroVerde.Maintenance.Application.Interface;
using OuroVerde.Maintenance.Domain.Adapters.Repository;

namespace OuroVerde.Maintenance.Application.AppServices
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : class
    {
        protected readonly IRepositoryBase<TEntity> _repository;

        public AppServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public void Add(TEntity entity)
        {
            _repository.Add(entity);
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
    }
}