namespace BuildingBlocks.Shared.Pagination;

/// <summary>
/// Represents a paginated request with page index and page size properties.
/// </summary>
public interface IPaginatedRequest
{
    /// <summary>
    /// Gets the page index for pagination.
    /// </summary>
    int PageIndex { get; }

    /// <summary>
    /// Gets the page size for pagination.
    /// </summary>
    int PageSize { get; }
}