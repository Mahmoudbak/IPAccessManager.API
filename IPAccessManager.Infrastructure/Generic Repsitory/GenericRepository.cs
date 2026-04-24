using IPAccessManager.Core.Entity;
using IPAccessManager.Core.Repository.contrent;
using IPAccessManager.Core.specifications;
using IPAccessManager.Infrastructure.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAccessManager.Infrastructure.Generic_Repsitory
{
    public class GenericRepository<T>:IGenaricrepository<T> where T:BaseEntity
    {
        protected static readonly ConcurrentDictionary<string, T> _store = new();




        public async Task<T?> GetByIdAsync(string id)
        {
            _store.TryGetValue(id, out var entity);
            return await Task.FromResult(entity); 
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var result = _store.Values.ToList();
            return await Task.FromResult(result);
        }


        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(Ispecifications<T> spec)
        {
            
            var query = _store.Values.AsQueryable();

            
            var result = SpecificationEvaluation<T>.GetQuery(query, spec).ToList();

            return await Task.FromResult(result);
        }


        public async Task AddAsync(T entity)
        {
            _store.TryAdd(entity.Id, entity);
            await Task.CompletedTask;
        }
        public async Task UpdateAsync(T entity)
        {
            _store[entity.Id] = entity;
            await Task.CompletedTask;

        }

        public async Task DeleteAsync(string id)
        {
            _store.TryRemove(id, out _);
            await Task.CompletedTask;
        }
    }
}
