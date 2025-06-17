namespace DiscussionService.Application.Pagination;

public class PagedResult<T> (IEnumerable<T> items, long total)
{
    public IEnumerable<T> Items { get; } = items;
    public long Total { get; set; } = total;
}