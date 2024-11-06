using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Shared.Persistence.Extensions;

public static class WebApplicationExtensions
{
	/// <summary>
	/// Asynchronously performs migrations for the specified database context type.
	/// </summary>
	/// <typeparam name="TDbContext">The type of the database context.</typeparam>
	/// <param name="webApplication">The instance of the application for which the migration will be executed.</param>
	/// <returns>A task representing the result of the asynchronous operation.</returns>
	public static async Task MigrateAsync<TDbContext>(this WebApplication webApplication)
		where TDbContext : DbContext
	{
		if (MigrationSkipRequested()) return;

		await using var scope = webApplication.Services.CreateAsyncScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
		await dbContext.Database.MigrateAsync();
	}

	/// <summary>
	/// Checks if the migration should be skipped based on the environment variable.
	/// </summary>
	/// <returns>True if migration should be skipped; otherwise, false.</returns>
	private static bool MigrationSkipRequested()
	{
		return bool.TryParse(Environment.GetEnvironmentVariable("SKIP_MIGRATION"), out var skipRequested) &&
		       skipRequested;
	}
}
