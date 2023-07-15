using Microsoft.EntityFrameworkCore;
using Noon.Core.Entities;
using Noon.Core.Specifications;

namespace Noon.Repository
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            if (specification?.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification?.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);
            if (specification?.OrderByDescending is not null)
                query = query.OrderByDescending(specification.OrderByDescending);


            if (specification is {IsPagination:true})
            {
                query = query.Skip(specification.Skip);
                query = query.Take(specification.Take);
            }

            query = specification?.IncludesExpressions.Aggregate(query,
                (currentQuery, includes) => currentQuery.Include(includes));



            return query!;
        }
    }
}
