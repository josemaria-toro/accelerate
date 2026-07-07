using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Data.Abstractions;

public partial class BaseRepository<TEntity>
{
    public void Insert(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The provided entity to insert must be a valid instance", nameof(entity));
        }

        try
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            _logger.LogDebug($"Inserting a new entity with id '{entity.Id}'");

            _entities.Add(entity);
        }
        catch (Exception ex)
        {
            throw new DataException("Unexpected error is encountered while inserting a new entity", ex);
        }
    }
    public async Task InsertAsync(TEntity entity,
                                  CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Insert(entity), cancellationToken);
    }
    public void Insert(IList<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The provided collection of entities to insert must be a valid instance", nameof(entities));
        }

        _logger.LogDebug($"Inserting a set of entities");

        foreach (var entity in entities)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            _logger.LogDebug($"Inserting a new entity with id '{entity.Id}'");
            _entities.Add(entity);
        }

        SavePendingChanges();
    }
    public async Task InsertAsync(IList<TEntity> entities,
                                  CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Insert(entities), cancellationToken);
    }
}