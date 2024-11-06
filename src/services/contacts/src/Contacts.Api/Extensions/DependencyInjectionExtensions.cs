using BuildingBlocks.Shared.Persistence.Extensions;
using Contacts.Application.Contacts;
using Contacts.Application.Contacts.Interfaces;
using Contacts.Application.Contacts.Services;
using Contacts.Application.Persistence.ApplicationDb;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Api.Extensions;

/// <summary>
/// Extension methods for registering application and infrastructure services in the dependency injection container.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Registers services for the application layer, including AutoMapper and contact-related services.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    public static void RegisterApplicationLayerServices(this IServiceCollection services)
    {
        // Registers AutoMapper with ContactProfile mappings.
        services.AddAutoMapper(typeof(ContactProfile));
        
        // Registers the contact service implementation.
        services.AddScoped<IContactService, ContactService>();
    }

    /// <summary>
    /// Registers services for the infrastructure layer, including database context, controllers, Swagger, and API endpoints.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="configuration">The application configuration for accessing settings such as connection strings.</param>
    public static void RegisterInfrastructureLayerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Adds MVC controllers to the service collection.
        services.AddControllers();
        
        // Registers the application database context with a PostgreSQL connection.
        services.RegisterAppDbContext<ContactDbContext>(builder =>
        {
            builder.UseNpgsql(
                configuration.GetConnectionString("ContactsDb"));
        });
        
        // Adds support for API endpoint documentation and exploration.
        services.AddEndpointsApiExplorer();
        
        // Registers Swagger for API documentation generation.
        services.AddSwaggerGen();
    }
}