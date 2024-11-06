using Contacts.Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contacts.Application.Persistence.Configuration;

/// <summary>
/// Provides configuration settings for the <see cref="Contact"/> entity,
/// implementing the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
/// </summary>
public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="Contact"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the <see cref="Contact"/> entity.</param>
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        // Sets the primary key for the Contact entity.
        builder.HasKey(q => q.Id);
    }
}
