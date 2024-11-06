using AutoMapper;
using Contacts.Application.Contacts.Models;
using Contacts.Domain.Contacts;

namespace Contacts.Application.Contacts;

/// <summary>
/// Profile configuration for AutoMapper to map between Contact-related models.
/// </summary>
public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, ContactViewModel>();
        CreateMap<AddContactModel, Contact>();
        CreateMap<UpdateContactModel, Contact>();
    }
}