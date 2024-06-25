namespace BuildingBlocks.Application.Features;

public class PagedList<T>(int pageSize, int pageNumber, int totalCount, IReadOnlyList<T> data)
{
    public int PageSize { get; set; } = pageSize;
    public int PageNumber { get; set; } = pageNumber;
    public int TotalCount { get; set; } = totalCount;
    public IEnumerable<T> Data { get; set; } = data;

    public static PagedList<T> Create(int pageSize, int pageNumber, int totalCount, IReadOnlyList<T> data)
    {
        return new PagedList<T>(pageSize, pageNumber, totalCount, data);
    }
}
