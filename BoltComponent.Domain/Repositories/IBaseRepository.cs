using System.Linq.Expressions;

namespace BoltComponent.Domain.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> Create(T entity);
    Task Update(T entity);
    Task<T> GetOne(Expression<Func<T, bool>> expression);
    Task<List<T>> GetAll();
    Task<bool> Exists(Expression<Func<T, bool>> expression);
    Task SaveChangesAsync();
    Task Delete(Expression<Func<T, bool>> expression);
}