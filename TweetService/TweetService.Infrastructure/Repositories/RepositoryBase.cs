using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.Pagination;

namespace TweetService.Infrastructure.Repositories;

public abstract class RepositoryBase<T>(ApplicationContext context) : 
    IRepositoryBase<T> where T : class
{
    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<T>> FindAllAsync(bool trackChanges, CancellationToken cancellationToken)
    {
        IQueryable<T> query = context.Set<T>();
        if (!trackChanges)
        {
            query.AsNoTracking();
        }
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges, CancellationToken cancellationToken)
    {
        IQueryable<T> query = context.Set<T>();
        if (!trackChanges)
        {
            query.AsNoTracking();
        }
        query = query.Where(expression);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<T>> GetByPageAsync(PageParams pageParams, bool trackChanges, CancellationToken cancellationToken)
    {
        IQueryable<T> query = context.Set<T>();
        if (!trackChanges)
        {
            query.AsNoTracking();
        }
        
        var totalCount = query.Count();
        var page = pageParams.Page;
        var pageSize = pageParams.PageSize;
        var skip = (page - 1) * pageSize;
        query = query.Skip(skip).Take(pageSize);
        return new PagedResult<T>(await query.ToListAsync(cancellationToken), totalCount);
    }
}