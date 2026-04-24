using IPAccessManager.Core.Entity;
using System.Linq.Expressions;

namespace IPAccessManager.Core.specifications
{
    public interface Ispecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public Expression<Func<T, object>>? OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}