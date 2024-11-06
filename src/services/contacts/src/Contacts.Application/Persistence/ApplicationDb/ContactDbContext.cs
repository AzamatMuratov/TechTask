using Contacts.Application.Persistence.Configuration;
using Contacts.Domain.Contacts;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Application.Persistence.ApplicationDb;

public class ContactDbContext(DbContextOptions<ContactDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
    }
}