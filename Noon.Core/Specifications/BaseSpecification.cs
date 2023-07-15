using System.Linq.Expressions;
using Noon.Core.Entities;

namespace Noon.Core.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {


        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> IncludesExpressions { get; set; } = new();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }


        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;

        }

        protected BaseSpecification()
        {

        }


        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }

        protected void ApplyPagination(int skip, int take)
        {
            IsPagination = true;

            Skip = skip;
            Take = take;
        }
    }
}
