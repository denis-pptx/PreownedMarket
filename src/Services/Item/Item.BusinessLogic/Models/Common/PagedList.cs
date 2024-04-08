using Microsoft.EntityFrameworkCore;

namespace Item.BusinessLogic.Models.Common;

public class PagedList<T>
{
    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
    }

    public List<T> Items { get; }

    public int Page { get; }

    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        if (page <= 0)
            page = 1;

        if (pageSize <= 0)
            pageSize = 5;

        var totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new(items, page, pageSize, totalCount);
    }
}