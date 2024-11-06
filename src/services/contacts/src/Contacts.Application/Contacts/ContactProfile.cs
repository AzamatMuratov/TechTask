using AutoMapper;
using Contacts.Application.Contacts.Models;
using Products.Domain.Contacts;

namespace Contacts.Application.Contacts;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, ContactViewModel>();
        CreateMap<AddContactModel, Contact>();
        CreateMap<UpdateContactModel, Contact>();
    }
}