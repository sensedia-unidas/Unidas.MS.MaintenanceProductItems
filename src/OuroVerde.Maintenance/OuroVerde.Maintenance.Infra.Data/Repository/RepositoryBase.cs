using Microsoft.EntityFrameworkCore;
using OuroVerde.Maintenance.Domain.Adapters.Repository;
using OuroVerde.Maintenance.Domain.Core.Domain;
using OuroVerde.Maintenance.Infra.Data.Context;

namespace OuroVerde.Maintenance.Infra.Data.Repository
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : Entity
    {
        protected readonly SensediaContext db;
        protected readonly DbSet<TEntity> dbSet;

        public RepositoryBase(SensediaContext context)
        {
            db = context;
            dbSet = db.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id) => await dbSet.FindAsync(id);

        public void Remove(Guid id)
        {
            TEntity obj = GetById(id).Result;
            dbSet.Remove(obj);
        }

        public void Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            dbSet.Update(entity);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

