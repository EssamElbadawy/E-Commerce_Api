using System.Linq.Expressions;
using Noon.Core.Entities;

namespace Noon.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }  // condition  .Include(p=> p.type)

        public List<Expression<Func<T, object>>> IncludesExpressions { get; set; }

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }

        public int Skip { get; set; }   

        public int Take { get; set; }


        public bool IsPagination { get; set; }  
    }
}
