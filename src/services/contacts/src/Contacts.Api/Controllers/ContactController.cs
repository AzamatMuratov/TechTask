using BuildingBlocks.Shared.Pagination;
using Contacts.Application.Contacts.Interfaces;
using Contacts.Application.Contacts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController(IContactService service) : ControllerBase
{
    [HttpGet]
    public async Task<PaginatedResult<ContactViewModel>> GetListAsync(
        [FromQuery] PaginatedRequest paginatedRequest,
        CancellationToken cancellationToken)
    {
        return await service.GetListAsync(paginatedRequest, cancellationToken);
    }

    [HttpPost]
    public async Task<Guid> AddAsync(
        [FromBody] AddContactModel addModel,
        CancellationToken cancellationToken)
    {
        return await service.AddAsync(addModel, cancellationToken);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateContactModel updateModel,
        CancellationToken cancellationToken)
    { 
        await service.UpdateAsync(id, updateModel, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}