using System.Collections;
using Noon.Core;
using Noon.Core.Entities;
using Noon.Core.Repositories;
using Noon.Repository.Data;

namespace Noon.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private Hashtable _repositories;
        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;

            if (_repositories.Contains(type)) return (_repositories[type] as IGenericRepository<TEntity>)!;

            var repository = new GenericRepository<TEntity>(_dataContext);
            _repositories.Add(type, repository);

            return (_repositories[type] as IGenericRepository<TEntity>)!;


        }

        public async Task<int> Complete()
            => await _dataContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dataContext.DisposeAsync();
    }
}
