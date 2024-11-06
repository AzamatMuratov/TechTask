using BuildingBlocks.Shared.Pagination;
using Contacts.Application.Contacts.Models;

namespace Contacts.Application.Contacts.Interfaces;

public interface IContactService
{
    public Task<PaginatedResult<ContactViewModel>> GetListAsync(
        PaginatedRequest paginatedRequest,
        CancellationToken cancellationToken = default);

    public Task<Guid> AddAsync(
        AddContactModel model,
        CancellationToken cancellationToken = default);

    public Task UpdateAsync(
        Guid entityId,
        UpdateContactModel model,
        CancellationToken cancellationToken = default);

    public Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}