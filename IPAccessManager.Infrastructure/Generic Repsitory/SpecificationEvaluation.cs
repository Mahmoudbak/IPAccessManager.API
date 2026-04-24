using IPAccessManager.Core.Entity;
using IPAccessManager.Core.specifications;

namespace IPAccessManager.Infrastructure.Repositories
{
    public class SpecificationEvaluation<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, Ispecifications<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.criteria is not null)
                query = query.Where(spec.criteria);

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);

            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

          

            return query;
        }
    }
}