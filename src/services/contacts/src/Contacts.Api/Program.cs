using BuildingBlocks.Shared.Configuration;
using BuildingBlocks.Shared.Persistence.Extensions;
using Contacts.Api.Extensions;
using Contacts.Application.Persistence.ApplicationDb;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureAppsettings();

builder.Services.RegisterApplicationLayerServices();
builder.Services.RegisterInfrastructureLayerServices(builder.Configuration);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});

var app = builder.Build();

await app.MigrateAsync<ContactDbContext>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();