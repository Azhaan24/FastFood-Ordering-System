using System.Linq.Expressions;

namespace FastFood.Core.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate);

        Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllWithIncludeAsync(
            params Expression<Func<T, object>>[] includes);

        Task<T?> GetByIdWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        IQueryable<T> Query();

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        IQueryable<T> AsQueryable();
    }
}