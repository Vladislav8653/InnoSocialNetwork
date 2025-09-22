using System.Linq.Expressions;
using TweetService.Application.Pagination;

namespace TweetService.Application.Contracts.RepositoryContracts;

public interface IRepositoryBase<T>
{
    public Task CreateAsync(T entity, CancellationToken cancellationToken);
    
    public Task UpdateAsync(T entity, CancellationToken cancellationToken);
    
    public Task DeleteAsync(T entity, CancellationToken cancellationToken);
    
    public Task<IEnumerable<T>> FindAllAsync(bool trackChanges, CancellationToken cancellationToken);
    public Task<IEnumerable<T>> FindByConditionAsync(
        Expression<Func<T, bool>> expression,
        bool trackChanges,
        CancellationToken cancellationToken);
    public Task<PagedResult<T>> GetByPageAsync(PageParams pageParams, bool trackChanges,
        CancellationToken cancellationToken);
}