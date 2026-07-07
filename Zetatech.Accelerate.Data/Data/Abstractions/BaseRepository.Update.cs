using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Data.Abstractions;

public partial class BaseRepository<TEntity>
{
    public void Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The provided entity to update must be a valid instance", nameof(entity));
        }

        _logger.LogDebug($"Updating the entity with id {entity.Id}");

        try
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            entity.UpdatedAt = DateTime.UtcNow;

            _entities.Update(entity);
        }
        catch (Exception ex)
        {
            throw new DataException($"Unexpected error is encountered while updating the entity with id {entity.Id}", ex);
        }
    }
    public async Task UpdateAsync(TEntity entity,
                                  CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Update(entity), cancellationToken);
    }
    public void Update(IList<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The provided collection of entities to update must be a valid instance", nameof(entities));
        }

        _logger.LogDebug("Updating a set of entities");

        foreach (var entity in entities)
        {
            _logger.LogDebug($"Updating the entity with id {entity.Id}");

            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            entity.UpdatedAt = DateTime.UtcNow;

            _entities.Update(entity);
        }
    }
    public async Task UpdateAsync(IList<TEntity> entities,
                                  CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Update(entities), cancellationToken);
    }
}