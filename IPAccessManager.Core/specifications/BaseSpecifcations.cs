using IPAccessManager.Core.Entity;
using System.Linq.Expressions;

namespace IPAccessManager.Core.specifications
{
    public class BaseSpecifcations<T> : Ispecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>>? OrderBy { get; set; } = null;
        public Expression<Func<T, object>>? OrderByDesc { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; } = false;

        public BaseSpecifcations()
        {
        }

        public BaseSpecifcations(Expression<Func<T, bool>> criteriaExpression)
        {
            criteria = criteriaExpression;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}