using System.Linq.Expressions;
using TweetService.Application.Pagination;

namespace TweetService.Application.Contracts.RepositoryContracts;

public interface IRepositoryBase<T>
{
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    void Delete(T entity);
    void Update(T entity);
    public Task<IEnumerable<T>> FindAllAsync(bool trackChanges, CancellationToken cancellationToken);
    public Task<IEnumerable<T>> FindByConditionAsync(
        Expression<Func<T, bool>> expression,
        bool trackChanges,
        CancellationToken cancellationToken);
    public Task<PagedResult<T>> GetByPageAsync(PageParams pageParams, bool trackChanges,
        CancellationToken cancellationToken);
}