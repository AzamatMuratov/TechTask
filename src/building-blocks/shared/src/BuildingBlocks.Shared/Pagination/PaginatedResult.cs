namespace BuildingBlocks.Shared.Pagination;

public class PaginatedResult<T>
{
    /// <summary>
    /// Constructor to create an instance of <see cref="PaginatedResult{T}"/>.
    /// </summary>
    /// <param name="items">An array of elements of type <typeparamref name="T"/> representing the data of a single page.</param>
    /// <param name="total">The total count of items matching the query criteria.</param>
    public PaginatedResult(T[] items, long total)
    {
        Items = items;
        Total = total;
    }

    /// <summary>
    /// Gets the array of items on the current page.
    /// </summary>
    /// <value>An array of elements of type <typeparamref name="T"/>.</value>
    public T[] Items { get; init; }

    /// <summary>
    /// Gets or sets the total count of items matching the query criteria.
    /// </summary>
    /// <value>The total count of items.</value>
    public long Total { get; set; }
}
