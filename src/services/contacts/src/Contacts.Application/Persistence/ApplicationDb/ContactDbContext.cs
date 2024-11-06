using Contacts.Application.Persistence.Configuration;
using Contacts.Domain.Contacts;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Application.Persistence.ApplicationDb;

/// <summary>
/// Database context.
/// </summary>
/// <param name="options"></param>
public class ContactDbContext(DbContextOptions<ContactDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Represents the Contacts table in the database, containing contact information.
    /// </summary>
    public DbSet<Contact> Contacts { get; set; }

    /// <summary>
    /// Configures the model for the context, applying specific configurations for the Contact entity.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure the entity models.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Applies the configuration defined in the ContactConfiguration class for the Contact entity.
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
    }
}
