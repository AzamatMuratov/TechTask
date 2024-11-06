using BuildingBlocks.Shared.Persistence.Extensions;
using Contacts.Application.Contacts;
using Contacts.Application.Contacts.Interfaces;
using Contacts.Application.Contacts.Services;
using Contacts.Application.Persistence.ApplicationDb;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static void RegisterApplicationLayerServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ContactProfile));
        services.AddScoped<IContactService, ContactService>();
    }

    public static void RegisterInfrastructureLayerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.RegisterAppDbContext<ContactDbContext>(builder =>
        {
            builder.UseNpgsql(
                configuration.GetConnectionString("ContactsDb"));
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}