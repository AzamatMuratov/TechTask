using Contacts.Application.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Contacts;

namespace Contacts.Application.Persistence.ApplicationDb;

public class ContactDbContext(DbContextOptions<ContactDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
    }
}