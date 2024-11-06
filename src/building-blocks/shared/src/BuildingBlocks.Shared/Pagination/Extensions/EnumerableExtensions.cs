namespace BuildingBlocks.Shared.Pagination.Extensions;

public static class EnumerableExtensions
{
	/// <summary>
	/// Applies pagination to an <see cref="IEnumerable{T}"/> sequence.
	/// </summary>
	/// <typeparam name="T">The type of elements in the sequence.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="request">Pagination parameters, including page index and page size.</param>
	/// <returns>A subsequence of the source sequence that matches the specified pagination parameters.</returns>
	private static IEnumerable<T> Paginated<T>(
		this IEnumerable<T> source,
		IPaginatedRequest request) =>
		source
			.Skip(Math.Max(request.PageIndex - 1, 0) * request.PageSize)
			.Take(request.PageSize);

	/// <summary>
	/// Converts an <see cref="IEnumerable{T}"/> sequence to a <see cref="PaginatedResult{T}"/> object,
	/// applying pagination.
	/// </summary>
	/// <typeparam name="T">The type of elements in the sequence.</typeparam>
	/// <param name="source">The source sequence.</param>
	/// <param name="request">Pagination parameters, including page index and page size.</param>
	/// <returns>A <see cref="PaginatedResult{T}"/> object containing the elements of the specified page and the total
	/// count of elements in the sequence.</returns>
	public static PaginatedResult<T> ToPaginatedResult<T>(
		this IEnumerable<T> source,
		IPaginatedRequest request)
	{
		var materialized = source.ToArray();

		var items = materialized.Paginated(request).ToArray();
		return new PaginatedResult<T>(items, materialized.Length);
	}
}