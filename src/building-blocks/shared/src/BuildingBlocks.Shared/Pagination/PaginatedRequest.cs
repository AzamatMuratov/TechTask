namespace BuildingBlocks.Shared.Pagination;

/// <summary>
/// Represents a paginated request with page index and page size parameters.
/// </summary>
public class PaginatedRequest : IPaginatedRequest
{
    private readonly int _index;

    /// <summary>
    /// The default value for an instance of <see cref="PaginatedRequest"/>.
    /// </summary>
    public static PaginatedRequest Default { get; } = new()
    {
        PageIndex = 1,
        PageSize = 10
    };
    
    /// <summary>
    /// Gets the page index for pagination. Must be 0 or greater.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than 0.</exception>
    public int PageIndex
    {
        get => _index;
        private init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "PageIndex cannot be less than 0.");
            }

            _index = value;
        }
    }
    
    /// <summary>
    /// Gets the page size for pagination. Default value is 10.
    /// </summary>
    public int PageSize { get; private init; } = 10;
}