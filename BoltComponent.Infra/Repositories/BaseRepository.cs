using System.Linq.Expressions;
using BoltComponent.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BoltComponent.Infra.Repositories;

public class BaseRepository<T>(BoltComponentContext context) : IBaseRepository<T>
    where T : class
{
    public async Task<T> Create(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task<List<T>> GetAll()
        => await context.Set<T>().ToListAsync();

    public async Task<T> GetOne(Expression<Func<T, bool>> expression)
        => (await context.Set<T>().AsNoTracking().SingleOrDefaultAsync(expression))!;

    public virtual async Task Update(T entity)
    {
        context.Set<T>().Update(entity);
        await SaveChangesAsync();
    }

    public async Task<bool> Exists(Expression<Func<T, bool>> expression)
        => await context.Set<T>().AsNoTracking().Where(expression).AnyAsync();

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task Delete(Expression<Func<T, bool>> expression)
        => await context.Set<T>().AsNoTracking().Where(expression).ExecuteDeleteAsync();
}