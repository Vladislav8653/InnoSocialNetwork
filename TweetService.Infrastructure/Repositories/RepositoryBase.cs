using Microsoft.EntityFrameworkCore;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.Pagination;

namespace TweetService.Infrastructure.Repositories;

public abstract class RepositoryBase<T>(ApplicationContext repositoryContext) : 
    IRepositoryBase<T> where T : class
{
    public async Task CreateAsync(T entity, CancellationToken cancellationToken) =>
        await repositoryContext.Set<T>().AddAsync(entity, cancellationToken);
    public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);
    public void Update(T entity) => repositoryContext.Set<T>().Update(entity);
    protected async Task<PagedResult<T>> GetByPageAsync(IQueryable<T> query, PageParams pageParams, CancellationToken cancellationToken)
    {
        var totalCount = query.Count();
        var page = pageParams.Page;
        var pageSize = pageParams.PageSize;
        var skip = (page - 1) * pageSize;
        query = query.Skip(skip).Take(pageSize);
        return new PagedResult<T>(await query.ToListAsync(cancellationToken), totalCount);
    }
}