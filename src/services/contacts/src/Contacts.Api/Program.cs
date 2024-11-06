using BuildingBlocks.Shared.Configuration;
using BuildingBlocks.Shared.Persistence.Extensions;
using Contacts.Api.Extensions;
using Contacts.Application.Persistence.ApplicationDb;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureAppsettings();  // Configures the application settings.

builder.Services.RegisterApplicationLayerServices(); // Register application layer services 
builder.Services.RegisterInfrastructureLayerServices(builder.Configuration); // Register infrastructure layer services

// Configures Kestrel server settings based on configuration in appsettings.
builder.WebHost.ConfigureKestrel(options =>
{
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});

var app = builder.Build();

// Applies pending migrations to the ContactDbContext database during startup.
await app.MigrateAsync<ContactDbContext>();

// Enables Swagger for API documentation generation.
app.UseSwagger();
app.UseSwaggerUI();

// Maps all controller endpoints.
app.MapControllers();

app.Run();