namespace TweetService.Infrastructure.Pagination;

public class PageParams(int page, int pageSize)
{
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}