using IPAccessManager.Core.Entity;
using IPAccessManager.Core.specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Core.Repository.contrent
{
    public interface IGenaricrepository<T>where T:BaseEntity
    {
        Task<T?> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(Ispecifications<T> spec);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string Id);
    }
}
