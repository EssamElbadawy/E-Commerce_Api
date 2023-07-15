using Noon.Core.Entities;
using Noon.Core.Specifications;

namespace Noon.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync(); 
        
        Task<T?> GetEntityWithSpecAsync(ISpecification<T> specification);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> specification);
        Task<int> GetCountWithAsync(ISpecification<T> specification);


        Task Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
    