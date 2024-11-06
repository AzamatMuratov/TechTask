using BuildingBlocks.Shared.Pagination;
using Contacts.Application.Contacts.Models;

namespace Contacts.Application.Contacts.Interfaces;

/// <summary>
/// Service interface for managing contacts, providing CRUD operations and pagination.
/// </summary>
public interface IContactService
{
    /// <summary>
    /// Retrieves a paginated list of contacts.
    /// </summary>
    /// <param name="paginatedRequest">Pagination parameters for the request.</param>
    /// <param name="cancellationToken">Optional token to cancel the asynchronous operation.</param>
    /// <returns>A paginated result containing a list of <see cref="ContactViewModel"/>.</returns>
    public Task<PaginatedResult<ContactViewModel>> GetListAsync(
        PaginatedRequest paginatedRequest,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new contact.
    /// </summary>
    /// <param name="model">Model containing information for the new contact.</param>
    /// <param name="cancellationToken">Optional token to cancel the asynchronous operation.</param>
    /// <returns>The unique identifier of the newly created contact.</returns>
    public Task<Guid> AddAsync(
        AddContactModel model,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing contact by ID.
    /// </summary>
    /// <param name="entityId">The unique identifier of the contact to update.</param>
    /// <param name="model">Model containing updated information for the contact.</param>
    /// <param name="cancellationToken">Optional token to cancel the asynchronous operation.</param>
    public Task UpdateAsync(
        Guid entityId,
        UpdateContactModel model,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a contact by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the contact to delete.</param>
    /// <param name="cancellationToken">Optional token to cancel the asynchronous operation.</param>
    public Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}