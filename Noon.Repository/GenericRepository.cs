using Microsoft.EntityFrameworkCore;
using Noon.Core.Entities;
using Noon.Core.Repositories;
using Noon.Core.Specifications;
using Noon.Repository.Data;

namespace Noon.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> GetAsync(int id)
          => await _context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> specification)
        {
            return await ApplySpecifications(specification).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> specification)
        {
            return await ApplySpecifications(specification).ToListAsync();
        }

        public async Task<int> GetCountWithAsync(ISpecification<T> specification)
        {
            return await ApplySpecifications(specification).CountAsync();
        }

        public async Task Add(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);


        private IQueryable<T> ApplySpecifications(ISpecification<T> specifications)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specifications);
        }
    }
}
