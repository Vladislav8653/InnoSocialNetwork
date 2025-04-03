namespace TweetService.Application.Contracts.RepositoryContracts;

public interface IRepositoryBase<T>
{
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    void Delete(T entity);
    void Update(T entity);
}