using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Shared.Pagination.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Returns the query results for the specified page.
    /// </summary>
    /// <typeparam name="T">The type of the database entity.</typeparam>
    /// <param name="source">The source data.</param>
    /// <param name="request">Page parameters.</param>
    /// <returns>A query with an added filter to select the specified page.</returns>
    private static IQueryable<T> Paginated<T>(
        this IQueryable<T> source,
        IPaginatedRequest request)
        => source
            .Skip(Math.Max(request.PageIndex - 1, 0) * request.PageSize)
            .Take(request.PageSize);

    /// <summary>
    /// Returns the query results for the specified page asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the database entity.</typeparam>
    /// <param name="source">The source data.</param>
    /// <param name="range">Page parameters.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>A query with an added filter to select the specified page.</returns>
    public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
        this IQueryable<T> source,
        IPaginatedRequest range,
        CancellationToken token = default)
    {
        var items = await source.Paginated(range).ToArrayAsync(token);
        var count = await source.CountAsync(token);
        return new PaginatedResult<T>(items, count);
    }
}