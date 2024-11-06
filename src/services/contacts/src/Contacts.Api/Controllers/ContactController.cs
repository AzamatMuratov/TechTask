using BuildingBlocks.Shared.Pagination;
using Contacts.Application.Contacts.Interfaces;
using Contacts.Application.Contacts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController(IContactService service) : ControllerBase
{
    /// <summary>
    /// Retrieves a paginated list of contacts.
    /// </summary>
    /// <param name="paginatedRequest">The pagination and filtering request parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>A paginated list of contacts as a <see cref="PaginatedResult{ContactViewModel}"/>.</returns>
    [HttpGet]
    public async Task<PaginatedResult<ContactViewModel>> GetListAsync(
        [FromQuery] PaginatedRequest paginatedRequest,
        CancellationToken cancellationToken)
    {
        return await service.GetListAsync(paginatedRequest, cancellationToken);
    }

    /// <summary>
    /// Adds a new contact.
    /// </summary>
    /// <param name="addModel">The model containing information for the new contact.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    /// <returns>The unique identifier (GUID) of the newly created contact.</returns>
    [HttpPost]
    public async Task<Guid> AddAsync(
        [FromBody] AddContactModel addModel,
        CancellationToken cancellationToken)
    {
        return await service.AddAsync(addModel, cancellationToken);
    }
    
    /// <summary>
    /// Updates an existing contact by ID.
    /// </summary>
    /// <param name="id">The unique identifier (GUID) of the contact to update.</param>
    /// <param name="updateModel">The model containing updated information for the contact.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateContactModel updateModel,
        CancellationToken cancellationToken)
    { 
        await service.UpdateAsync(id, updateModel, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Deletes a contact by ID.
    /// </summary>
    /// <param name="id">The unique identifier (GUID) of the contact to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}