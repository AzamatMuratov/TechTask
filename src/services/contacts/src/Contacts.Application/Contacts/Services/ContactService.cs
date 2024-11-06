using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuildingBlocks.Shared.Pagination;
using BuildingBlocks.Shared.Pagination.Extensions;
using Contacts.Application.Contacts.Interfaces;
using Contacts.Application.Contacts.Models;
using Contacts.Application.Persistence.ApplicationDb;
using Contacts.Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contacts.Application.Contacts.Services;

public class ContactService(
    ContactDbContext dbContext,
    IMapper mapper,
    ILogger<ContactService> logger) : IContactService
{
    /// <inheritdoc />
    public async Task<PaginatedResult<ContactViewModel>> GetListAsync(
        PaginatedRequest paginatedRequest,
        CancellationToken cancellationToken = default)
    {
        var products = dbContext.Contacts.AsQueryable();

        return await products
            .ProjectTo<ContactViewModel>(mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToPaginatedResultAsync(paginatedRequest, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> AddAsync(
        AddContactModel addModel,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var product = mapper.Map<Contact>(addModel);

            await dbContext.AddAsync(product, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);
            
            logger.LogInformation(
                "[{EntityName}][{CrudMethod}] new entity entry added",
                nameof(Contact),
                "Add");

            return product.Id;
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "[{EntityName}][{CrudMethod}] error while trying to create entity, rolling back...\n{@EntityContent:ij}",
                nameof(Contact),
                "Add",
                addModel);
            
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }
    
    /// <inheritdoc />
    public async Task UpdateAsync(
        Guid entityId,
        UpdateContactModel updateModel,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var entity = await dbContext.Contacts
                .Where(x => x.Id == entityId)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                throw new KeyNotFoundException($"Product with Id {entityId} was not found.");
            }

            mapper.Map(updateModel, entity);

            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            logger.LogInformation(
                "[{EntityName}][{CrudMethod}] entity entry updated\n{@EntityContent:ij}",
                nameof(Contact),
                "Update",
                updateModel);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "[{EntityName}][{CrudMethod}] error while trying to update entity, rolling back...\n{@EntityContent:ij}",
                nameof(Contact),
                "Update",
                updateModel);

            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }

    /// <inheritdoc />
    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var entity = await dbContext.Contacts
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (entity is null)
            {
                throw new KeyNotFoundException($"Product with Id {id} was not found.");
            }

            dbContext.Contacts.Remove(entity);

            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            logger.LogInformation(
                "[{EntityName}][{CrudMethod}] entity entry deleted {EntityId}",
                nameof(Contact),
                "Delete",
                id);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "[{EntityName}][{CrudMethod}] error while trying to delete entity with id: {EntityId}, rolling back...",
                nameof(Contact),
                "Delete",
                id);

            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }
    
}