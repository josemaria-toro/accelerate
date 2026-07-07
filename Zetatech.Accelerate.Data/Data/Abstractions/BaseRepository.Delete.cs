using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Zetatech.Accelerate.Data.Abstractions;

public partial class BaseRepository<TEntity>
{
    public void Delete(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("The provided entity to delete must be a valid instance", nameof(entity));
        }

        _logger.LogDebug($"Deleting the entity with id '{entity.Id}'");

        try
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            _entities.Remove(entity);
        }
        catch (Exception ex)
        {
            throw new DataException($"Unexpected error is encountered while deleting the entity with id '{entity.Id}'", ex);
        }
    }
    public async Task DeleteAsync(TEntity entity,
                                  CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Delete(entity), cancellationToken);
    }
    public void Delete(IList<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentException("The provided set of entities to delete must be a valid instance", nameof(entities));
        }

        _logger.LogDebug("Deleting a set of entities");

        foreach (var entity in entities)
        {
            _logger.LogDebug($"Deleting the entity with id '{entity.Id}'");

            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            _entities.Remove(entity);
        }

        SavePendingChanges();
    }
    public async Task DeleteAsync(IList<TEntity> entities,
                                  CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Delete(entities), cancellationToken);
    }
    public void Delete(Expression<Func<TEntity, Boolean>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentException("The provided expression to determine the set of entities to delete must be a valid instance", nameof(expression));
        }

        _logger.LogDebug($"Deleting a set of entities after filtering the data source using a expression");

        var entities = _entities.Where(expression)
                                .ToList();

        foreach (var entity in entities)
        {
            _logger.LogDebug($"Deleting the entity with id '{entity.Id}'");

            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }

            _entities.Remove(entity);
        }

        SavePendingChanges();
    }
    public async Task DeleteAsync(Expression<Func<TEntity, Boolean>> expression,
                                  CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Delete(expression), cancellationToken);
    }
}