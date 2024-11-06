using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.Shared.Configuration;

public static class AppConfigurationExtensions
{
    /// <summary>
    ///     Configures the application settings.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    public static void ConfigureAppsettings(this IHostApplicationBuilder builder)
    {
        var env = builder.Environment;

        builder.Configuration
            .AddJsonFile("appsettings.json", false, true) // Adds the main settings file.
            .AddJsonFile(
                $"appsettings.{env.EnvironmentName}.json",
                true,
                true) // Adds an environment-specific settings file.
            .AddEnvironmentVariables(); // Adds environment variables as a configuration source.
    }
}