using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Shared.Persistence.Extensions;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Registers the application's database context with the specified options.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <param name="services">The service collection for component registration.</param>
    /// <param name="optionsBuilder">An action to configure the options for the database context.</param>
    public static void RegisterAppDbContext<TDbContext>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsBuilder)
        where TDbContext : DbContext
        => services.RegisterAppDbContext<TDbContext>((_, builder) => optionsBuilder(builder));

    /// <summary>
    /// Registers the application's database context with dependency configuration through the service provider.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <param name="services">The service collection for component registration.</param>
    /// <param name="optionsBuilder">An action to configure the options for the database context using the service provider.</param>
    private static void RegisterAppDbContext<TDbContext>(
        this IServiceCollection services,
        Action<IServiceProvider, DbContextOptionsBuilder> optionsBuilder)
        where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(optionsBuilder);
        services.AddScoped<DbContext>(provider => provider.GetRequiredService<TDbContext>());
    }
}